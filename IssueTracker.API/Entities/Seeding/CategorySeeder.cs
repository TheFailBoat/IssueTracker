using System.Data;
using System.Linq;
using ServiceStack.OrmLite;

namespace IssueTracker.API.Entities.Seeding
{
    internal class CategorySeeder
    {
        public static void Seed(IDbConnection db)
        {
            if (db.Select<CategoryEntity>().Any()) return;

            db.InsertParam(new CategoryEntity { Name = "Support", Colour = "" });
            db.InsertParam(new CategoryEntity { Name = "Project", Colour = "" });
            db.InsertParam(new CategoryEntity { Name = "Maintainance", Colour = "#ffa500" });
            db.InsertParam(new CategoryEntity { Name = "Sales", Colour = "#f0f0f0" });
        }
    }
}