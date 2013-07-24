using System.Data;
using System.Linq;
using ServiceStack.OrmLite;

namespace IssueTracker.API.Entities.Seeding
{
    internal class PrioritySeeder
    {
        public static void Seed(IDbConnection db)
        {
            if (db.Select<PriorityEntity>().Any()) return;

            db.InsertParam(new PriorityEntity { Name = "Low", Colour = "#3bb9ff", Order = 0 });
            db.InsertParam(new PriorityEntity { Name = "Normal", Colour = "", Order = 1 });
            db.InsertParam(new PriorityEntity { Name = "High", Colour = "#f88017", Order = 2 });
            db.InsertParam(new PriorityEntity { Name = "Urgent", Colour = "#800517", Order = 3 });
            db.InsertParam(new PriorityEntity { Name = "Immediate", Colour = "#ff0080", Order = 4 });
        }
    }
}