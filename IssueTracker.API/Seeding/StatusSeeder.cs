using System;
using System.Linq;
using Funq;
using IssueTracker.API.Repositories;
using IssueTracker.Data;

namespace IssueTracker.API.Seeding
{
    internal class StatusSeeder
    {
        public static void Seed(Container container)
        {
            var repo = container.Resolve<IStatusRepository>();
            if (repo.GetAll().Any()) return;

            repo.Add(new Status { Name = "New", Colour = "", IsClosed = false, Order = 1 });
            repo.Add(new Status { Name = "Scheduled", Colour = "", IsClosed = false, Order = 2 });
            repo.Add(new Status { Name = "In Progress", Colour = "", IsClosed = false, Order = 3 });
            repo.Add(new Status { Name = "Waiting for Feedback", Colour = "", IsClosed = false, Order = 4 });
            repo.Add(new Status { Name = "Resolved", Colour = "", IsClosed = true, Order = 5 });
            repo.Add(new Status { Name = "Closed", Colour = "", IsClosed = true, Order = 6 });
            repo.Add(new Status { Name = "Rejected", Colour = "", IsClosed = true, Order = 7 });
        }
    }
}