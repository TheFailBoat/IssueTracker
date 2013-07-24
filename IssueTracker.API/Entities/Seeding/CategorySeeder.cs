using System.Data;
using System.Linq;
using IssueTracker.Data;
using IssueTracker.Data.Issues.Categories;
using ServiceStack.OrmLite;

namespace IssueTracker.API.Seeding
{
    internal class CategorySeeder
    {
        public static void Seed(IDbConnection db)
        {
            if (db.Select<Category>().Any()) return;

            db.InsertParam(new Category { Name = "Support", Colour = "" });
            db.InsertParam(new Category { Name = "Project", Colour = "" });
            db.InsertParam(new Category { Name = "Maintainance", Colour = "#ffa500" });
            db.InsertParam(new Category { Name = "Sales", Colour = "#f0f0f0" });
        }
    }
}