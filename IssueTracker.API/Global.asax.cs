using System;
using System.Configuration;
using System.Web;
using Funq;
using IssueTracker.API.Repositories;
using IssueTracker.Data;
using ServiceStack.CacheAccess;
using ServiceStack.CacheAccess.Providers;
using ServiceStack.Configuration;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.ServiceInterface.Validation;
using ServiceStack.WebHost.Endpoints;

namespace IssueTracker.API
{
    public class Global : HttpApplication
    {
        public class IssueTrackerHost : AppHostBase
        {
            // Tell Service Stack the name of your application and where to find your web services
            public IssueTrackerHost() : base("IssueTracker Web Services", typeof(Global).Assembly) { }

            public override void Configure(Funq.Container container)
            {
                container.Register<ICacheClient>(new MemoryCacheClient());

                ConfigureAuth(container);
                ConfigureData(container);

                Plugins.Add(new ValidationFeature());
                container.RegisterValidators(typeof(Global).Assembly);
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
                        new BasicAuthProvider(),                    // Sign-in with Basic Auth
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

                authRepo.CreateUserAuth(new UserAuth
                {
                    UserName = appSettings.Get("DefaultAdminUsername", "admin"),
                    FirstName = "Default",
                    LastName = "Administrator"
                }, appSettings.Get("DefaultAdminPassword", "password"));
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
                    db.CreateTable<Person>(overwrite: false);
                    db.CreateTable<Issue>(overwrite: false);
                    db.CreateTable<Comment>(overwrite: false);
                    db.CreateTable<CommentChange>(overwrite: false);
                });

                // TODO Seed

                // TODO Repository
                container.Register<IIssueRepository>(new IssueRepository());
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

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}