using System.Collections.Generic;
using ServiceStack.ServiceHost;

namespace IssueTracker.Data.Requests.Categories
{
    [Route("/categories", "GET")]
    public class CategoriesList : IReturn<List<Category>>
    {
    }
}
