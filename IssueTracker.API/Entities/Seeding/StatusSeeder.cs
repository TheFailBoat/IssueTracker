using System.Data;
using System.Linq;
using IssueTracker.Data;
using IssueTracker.Data.Issues.Statuses;
using ServiceStack.OrmLite;

namespace IssueTracker.API.Seeding
{
    internal class StatusSeeder
    {
        public static void Seed(IDbConnection db)
        {
            if (db.Select<Status>().Any()) return;

            db.InsertParam(new Status { Name = "New", Colour = "", IsClosed = false, Order = 1 });
            db.InsertParam(new Status { Name = "Scheduled", Colour = "", IsClosed = false, Order = 2 });
            db.InsertParam(new Status { Name = "In Progress", Colour = "", IsClosed = false, Order = 3 });
            db.InsertParam(new Status { Name = "Waiting for Feedback", Colour = "", IsClosed = false, Order = 4 });
            db.InsertParam(new Status { Name = "Resolved", Colour = "", IsClosed = true, Order = 5 });
            db.InsertParam(new Status { Name = "Closed", Colour = "", IsClosed = true, Order = 6 });
            db.InsertParam(new Status { Name = "Rejected", Colour = "", IsClosed = true, Order = 7 });
        }
    }
}