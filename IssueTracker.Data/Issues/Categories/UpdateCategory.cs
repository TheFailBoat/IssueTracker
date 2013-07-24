using ServiceStack.ServiceHost;

namespace IssueTracker.Data.Issues.Categories
{
    [Route("/categories", "POST")]
    [Route("/categories/{id}", "PUT")]
    public class UpdateCategory : IReturn<UpdateCategoryResponse>
    {
        [ApiMember(Verb = "PUT")]
        public long Id { get; set; }

        public string Name { get; set; }
        public string Colour { get; set; }
    }

    public class UpdateCategoryResponse
    {
        public Category Category { get; set; }
    }
}