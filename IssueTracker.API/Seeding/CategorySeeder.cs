using System;
using System.Linq;
using Funq;
using IssueTracker.API.Repositories;
using IssueTracker.Data;

namespace IssueTracker.API.Seeding
{
    internal class CategorySeeder
    {
        public static void Seed(Container container)
        {
            var repo = container.Resolve<ICategoryRepository>();
            if (repo.GetAll().Any()) return;

            repo.Add(new Category { Name = "Support", Colour = "" });
            repo.Add(new Category { Name = "Project", Colour = "" });
            repo.Add(new Category { Name = "Maintainance", Colour = "#ffa500" });
            repo.Add(new Category { Name = "Sales", Colour = "#f0f0f0" });
        }
    }
}