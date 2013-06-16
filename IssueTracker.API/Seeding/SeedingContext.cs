using Funq;
using ServiceStack.OrmLite;

namespace IssueTracker.API.Seeding
{
    internal class SeedingContext
    {
        public static void Seed(IDbConnectionFactory dbFactory)
        {
            dbFactory.Run(db =>
            {
                CategorySeeder.Seed(db);
                PrioritySeeder.Seed(db);
                StatusSeeder.Seed(db);
            });
        }
    }
}