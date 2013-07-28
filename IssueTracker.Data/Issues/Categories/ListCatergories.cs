using System.Collections.Generic;
using ServiceStack.ServiceHost;

namespace IssueTracker.Data.Issues.Categories
{
    [Route("/categories", "GET,OPTIONS")]
    public class ListCategories : IReturn<ListCategoriesResponse>
    {
    }

    public class ListCategoriesResponse
    {
        public List<Category> Categories { get; set; }
    }
}
