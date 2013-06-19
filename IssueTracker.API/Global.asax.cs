using System;
using System.Configuration;
using System.Linq;
using System.Web;
using Funq;
using IssueTracker.API.Repositories;
using IssueTracker.API.Seeding;
using IssueTracker.Data;
using ServiceStack;
using ServiceStack.CacheAccess;
using ServiceStack.CacheAccess.Providers;
using ServiceStack.Common.Web;
using ServiceStack.Configuration;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.ServiceInterface.Cors;
using ServiceStack.ServiceInterface.Validation;
using ServiceStack.WebHost.Endpoints;
using ServiceStack.WebHost.Endpoints.Extensions;

namespace IssueTracker.API
{
    public class Global : HttpApplication
    {
        public class IssueTrackerHost : AppHostBase
        {
            // Tell Service Stack the name of your application and where to find your web services
            public IssueTrackerHost() : base("IssueTracker Web Services", typeof(Global).Assembly) { }

            public override void Configure(Container container)
            {
                container.Register<ICacheClient>(new MemoryCacheClient());

                ConfigureData(container);
                ConfigureAuth(container);
                ConfigureCors(container);

                Plugins.Add(new ValidationFeature());
                container.RegisterValidators(typeof(Global).Assembly);
            }

            private void ConfigureCors(Container container)
            {
                var appSettings = new AppSettings();

                if (!appSettings.Get("CorsEnabled", false)) return;

                var origins = appSettings.Get("CorsOrigins", "*");
                var methods = appSettings.Get("CorsMethods", "GET, POST, PUT, DELETE, OPTIONS");
                var headers = appSettings.Get("CorsHeaders", "Content-Type");
                var credentials = appSettings.Get("CorsCreds", false);

                if (!string.IsNullOrEmpty(origins))
                    Config.GlobalResponseHeaders.Add(HttpHeaders.AllowOrigin, origins);
                if (!string.IsNullOrEmpty(methods))
                    Config.GlobalResponseHeaders.Add(HttpHeaders.AllowMethods, methods);
                if (!string.IsNullOrEmpty(headers))
                    Config.GlobalResponseHeaders.Add(HttpHeaders.AllowHeaders, headers);
                if (credentials)
                    Config.GlobalResponseHeaders.Add(HttpHeaders.AllowCredentials, "true");

                PreRequestFilters.Add((httpReq, httpRes) =>
                {
                    if (httpReq.HttpMethod == "OPTIONS")
                    {
                        httpRes.ApplyGlobalResponseHeaders();
                        httpRes.End();
                    }
                });
            }

            private void ConfigureAuth(Container container)
            {
                var appSettings = new AppSettings();

                Plugins.Add(new AuthFeature(
                    () => new AuthUserSession(),
                    new IAuthProvider[] {
                        new CredentialsAuthProvider(),              // HTML Form post of UserName/Password credentials
                        new TwitterAuthProvider(appSettings),       // Sign-in with Twitter
                        new FacebookAuthProvider(appSettings),      // Sign-in with Facebook
                        new DigestAuthProvider(appSettings),        // Sign-in with Digest Auth
                        new BasicAuthProvider()                     // Sign-in with Basic Auth
                        // TODO Custom auth provider for AD integration
                }));

                container.Register<IUserAuthRepository>(c => new OrmLiteAuthRepository(c.Resolve<IDbConnectionFactory>()));

                var authRepo = (OrmLiteAuthRepository)container.Resolve<IUserAuthRepository>();
                if (appSettings.Get("RecreateAuthTables", false))
                {
                    authRepo.DropAndReCreateTables(); //Drop and re-create all Auth and registration tables
                }
                else
                {
                    authRepo.CreateMissingTables();   //Create only the missing tables
                }

                var defaultUsername = appSettings.Get("DefaultAdminUsername", "admin");
                if (authRepo.GetUserAuthByUserName(defaultUsername) == null)
                {
                    authRepo.CreateUserAuth(new UserAuth
                    {
                        UserName = defaultUsername,
                        FirstName = "Default",
                        LastName = "Administrator",
                        Roles = { Constants.EmployeeRoleName }
                    }, appSettings.Get("DefaultAdminPassword", "password"));
                }

                RequestFilters.Add((request, response, dto) =>
                                       {
                                           const string key = ServiceExtensions.RequestItemsSessionKey;
                                           HttpContext.Current.Items[key] = request.Items[key];
                                       });
            }

            private void ConfigureData(Container container)
            {
                var dbFactory = GetDataFactory();

                container.Register(dbFactory);

                dbFactory.Run(db =>
                {
                    db.CreateTable<Category>(overwrite: false);
                    db.CreateTable<Status>(overwrite: false);
                    db.CreateTable<Priority>(overwrite: false);
                    db.CreateTable<Customer>(overwrite: false);
                    db.CreateTable<Issue>(overwrite: false);
                    db.CreateTable<Comment>(overwrite: false);
                    db.CreateTable<CommentChange>(overwrite: false);
                });

                container.Register<ICategoryRepository>(c => new CategoryRepository(c.Resolve<IDbConnectionFactory>(), c.Resolve<IPersonRepository>())).ReusedWithin(ReuseScope.None);
                container.Register<ICommentRepository>(c => new CommentRepository(c.Resolve<IDbConnectionFactory>(), c.Resolve<IPersonRepository>(), c.Resolve<IIssueRepository>())).ReusedWithin(ReuseScope.None);
                container.Register<ICommentChangeRepository>(c => new CommentChangeRepository(c.Resolve<IDbConnectionFactory>(), c.Resolve<ICommentRepository>())).ReusedWithin(ReuseScope.None);
                container.Register<IIssueRepository>(c => new IssueRepository(c.Resolve<IDbConnectionFactory>(), c.Resolve<IPersonRepository>())).ReusedWithin(ReuseScope.None);
                container.Register<IPriorityRepository>(c => new PriorityRepository(c.Resolve<IDbConnectionFactory>(), c.Resolve<IPersonRepository>())).ReusedWithin(ReuseScope.None);
                container.Register<IStatusRepository>(c => new StatusRepository(c.Resolve<IDbConnectionFactory>(), c.Resolve<IPersonRepository>())).ReusedWithin(ReuseScope.None);

                container.Register<IPersonRepository>(c =>
                                                          {
                                                              const string key = ServiceExtensions.RequestItemsSessionKey;
                                                              var currentUser = HttpContext.Current.Items[key] as IAuthSession;

                                                              return new PersonRepository(c.Resolve<IUserAuthRepository>(), currentUser);
                                                          }).ReusedWithin(ReuseScope.None);

                SeedingContext.Seed(dbFactory);
            }

            private static IDbConnectionFactory GetDataFactory()
            {
                var appSettings = new AppSettings();

                var connStringName = appSettings.Get<string>("ConnectionString", null);
                if (connStringName == null)
                {
                    // TODO
                    return null;
                }

                var connString = ConfigurationManager.ConnectionStrings[connStringName];
                var provider = GetDialectProvider(connString.ProviderName);

                return new OrmLiteConnectionFactory(connString.ConnectionString, provider);
            }

            private static IOrmLiteDialectProvider GetDialectProvider(string providerName)
            {
                // TODO: add more providers as necessary
                switch (providerName.ToLower())
                {
                    default:
                        return new SqlServerOrmLiteDialectProvider();
                }
            }
        }


        protected void Application_Start(object sender, EventArgs e)
        {
            new IssueTrackerHost().Init();
        }

        public class Constants
        {
            public const string EmployeeRoleName = "employee";
        }
    }
}