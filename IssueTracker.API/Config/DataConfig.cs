using System.Configuration;
using Funq;
using IssueTracker.API.Seeding;
using ServiceStack.Configuration;
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

            //TODO register repositories

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
}