using System.Data;
using System.Linq;
using ServiceStack.OrmLite;

namespace IssueTracker.API.Entities.Seeding
{
    internal class StatusSeeder
    {
        public static void Seed(IDbConnection db)
        {
            if (db.Select<StatusEntity>().Any()) return;

            db.InsertParam(new StatusEntity { Name = "New", Colour = "", IsClosed = false, Order = 1 });
            db.InsertParam(new StatusEntity { Name = "Scheduled", Colour = "", IsClosed = false, Order = 2 });
            db.InsertParam(new StatusEntity { Name = "In Progress", Colour = "", IsClosed = false, Order = 3 });
            db.InsertParam(new StatusEntity { Name = "Waiting for Feedback", Colour = "", IsClosed = false, Order = 4 });
            db.InsertParam(new StatusEntity { Name = "Resolved", Colour = "", IsClosed = true, Order = 5 });
            db.InsertParam(new StatusEntity { Name = "Closed", Colour = "", IsClosed = true, Order = 6 });
            db.InsertParam(new StatusEntity { Name = "Rejected", Colour = "", IsClosed = true, Order = 7 });
        }
    }
}