using System.Linq;
using Funq;
using IssueTracker.API.Repositories;
using IssueTracker.Data;

namespace IssueTracker.API.Seeding
{
    internal class PrioritySeeder
    {
        public static void Seed(Container container)
        {
            var repo = container.Resolve<IPriorityRepository>();
            if (repo.GetAll().Any()) return;

            repo.Add(new Priority { Name = "Low", Colour = "#3bb9ff", Order = 0 });
            repo.Add(new Priority { Name = "Normal", Colour = "", Order = 1 });
            repo.Add(new Priority { Name = "High", Colour = "#f88017", Order = 2 });
            repo.Add(new Priority { Name = "Urgent", Colour = "#800517", Order = 3 });
            repo.Add(new Priority { Name = "Immediate", Colour = "#ff0080", Order = 4 });
        }
    }
}