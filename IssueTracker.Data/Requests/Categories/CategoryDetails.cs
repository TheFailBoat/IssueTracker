using ServiceStack.ServiceHost;

namespace IssueTracker.Data.Requests.Categories
{
    [Route("/categories/{id}", "GET")]
    public class CategoryDetails : IReturn<Category>
    {
        public long Id { get; set; }
    }
}
