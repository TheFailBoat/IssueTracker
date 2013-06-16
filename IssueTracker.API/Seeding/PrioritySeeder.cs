using System.Data;
using System.Linq;
using IssueTracker.Data;
using ServiceStack.OrmLite;

namespace IssueTracker.API.Seeding
{
    internal class PrioritySeeder
    {
        public static void Seed(IDbConnection db)
        {
            if (db.Select<Priority>().Any()) return;

            db.InsertParam(new Priority { Name = "Low", Colour = "#3bb9ff", Order = 0 });
            db.InsertParam(new Priority { Name = "Normal", Colour = "", Order = 1 });
            db.InsertParam(new Priority { Name = "High", Colour = "#f88017", Order = 2 });
            db.InsertParam(new Priority { Name = "Urgent", Colour = "#800517", Order = 3 });
            db.InsertParam(new Priority { Name = "Immediate", Colour = "#ff0080", Order = 4 });
        }
    }
}