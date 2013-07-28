using System.Configuration;
using System.Data;
using Funq;
using IssueTracker.API.Entities.Seeding;
using IssueTracker.API.Repositories;
using IssueTracker.API.Security;
using ServiceStack.Configuration;
using ServiceStack.MiniProfiler;
using ServiceStack.MiniProfiler.Data;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;

namespace IssueTracker.API.Config
{
    public class DataConfig
    {
        public static void Register(Container container)
        {
            // Register Data Connection Factory
            var dbFactory = GetDataFactory();
            container.Register(dbFactory);
            container.Register(x => x.Resolve<IDbConnectionFactory>().Open()).ReusedWithin(ReuseScope.Request);

            container.RegisterSecureRepository<IAuthTokenRepository>(x => new AuthTokenRepository(x.Resolve<IDbConnection>())).ReusedWithin(ReuseScope.Request);
            container.RegisterSecureRepository<ICategoryRepository>(x => new CategoryRepository(x.Resolve<IDbConnection>())).ReusedWithin(ReuseScope.Request);
            container.RegisterSecureRepository<ICommentChangeRepository>(x => new CommentChangeRepository(x.Resolve<IDbConnection>())).ReusedWithin(ReuseScope.Request);
            container.RegisterSecureRepository<ICommentRepository>(x => new CommentRepository(x.Resolve<IDbConnection>())).ReusedWithin(ReuseScope.Request);
            container.RegisterSecureRepository<IIssueRepository>(x => new IssueRepository(x.Resolve<IDbConnection>())).ReusedWithin(ReuseScope.Request);
            container.RegisterSecureRepository<IPriorityRepository>(x => new PriorityRepository(x.Resolve<IDbConnection>())).ReusedWithin(ReuseScope.Request);
            container.RegisterSecureRepository<IStatusRepository>(x => new StatusRepository(x.Resolve<IDbConnection>())).ReusedWithin(ReuseScope.Request);
            container.RegisterSecureRepository<IUserRepository>(x => new UserRepository(x.Resolve<IDbConnection>())).ReusedWithin(ReuseScope.Request);

            //container.RegisterInsecureRepository<IAuthTokenRepository>(x => new AuthTokenRepository(x.Resolve<IDbConnectionFactory>())).ReusedWithin(ReuseScope.Request);
            //container.RegisterInsecureRepository<ICategoryRepository>(x => new CategoryRepository(x.Resolve<IDbConnectionFactory>())).ReusedWithin(ReuseScope.Request);
            //container.RegisterInsecureRepository<ICommentChangeRepository>(x => new CommentChangeRepository(x.Resolve<IDbConnectionFactory>())).ReusedWithin(ReuseScope.Request);
            //container.RegisterInsecureRepository<ICommentRepository>(x => new CommentRepository(x.Resolve<IDbConnectionFactory>())).ReusedWithin(ReuseScope.Request);
            container.RegisterInsecureRepository<IIssueRepository>(x => new IssueRepository(x.Resolve<IDbConnection>())).ReusedWithin(ReuseScope.Request);
            //container.RegisterInsecureRepository<IPriorityRepository>(x => new PriorityRepository(x.Resolve<IDbConnectionFactory>())).ReusedWithin(ReuseScope.Request);
            //container.RegisterInsecureRepository<IStatusRepository>(x => new StatusRepository(x.Resolve<IDbConnectionFactory>())).ReusedWithin(ReuseScope.Request);
            //container.RegisterInsecureRepository<IUserRepository>(x => new UserRepository(x.Resolve<IDbConnectionFactory>())).ReusedWithin(ReuseScope.Request);

            // initialize the database
            DataInitializer.CreateTables(dbFactory);
            DataInitializer.SeedTables(dbFactory);
        }

        private static IDbConnectionFactory GetDataFactory()
        {
            var appSettings = new AppSettings();

            var connStringName = appSettings.Get<string>("ConnectionString", null);
            if (connStringName == null) return null;

            var connString = ConfigurationManager.ConnectionStrings[connStringName];
            var provider = GetDialectProvider(connString.ProviderName);

            return new OrmLiteConnectionFactory(connString.ConnectionString, provider)
            {
                ConnectionFilter = x => new ProfiledDbConnection(x, Profiler.Current)
            };
        }
        private static IOrmLiteDialectProvider GetDialectProvider(string providerName)
        {
            // add more providers as necessary
            switch (providerName.ToLower())
            {
                default:
                    return new SqlServerOrmLiteDialectProvider();
            }
        }

    }
}