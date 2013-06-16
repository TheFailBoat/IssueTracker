using Funq;

namespace IssueTracker.API.Seeding
{
    internal class SeedingContext
    {
        public static void Seed(Container container)
        {
            CategorySeeder.Seed(container);
            PrioritySeeder.Seed(container);
            StatusSeeder.Seed(container);
        }
    }
}