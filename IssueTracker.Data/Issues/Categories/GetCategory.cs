using ServiceStack.ServiceHost;

namespace IssueTracker.Data.Issues.Categories
{
    [Route("/categories/{id}", "GET")]
    public class GetCategory : IReturn<GetCategoryResponse>
    {
        [ApiMember]
        public long Id { get; set; }
    }

    public class GetCategoryResponse
    {
        public Category Category { get; set; }
    }
}
