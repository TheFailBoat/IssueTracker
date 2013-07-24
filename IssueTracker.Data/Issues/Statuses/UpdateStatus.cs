using ServiceStack.ServiceHost;

namespace IssueTracker.Data.Issues.Statuses
{
    [Route("/statuses", "POST")]
    [Route("/statuses/{id}", "PUT")]
    public class UpdateStatus : IReturn<UpdateStatusResponse>
    {
        [ApiMember(Verb = "PUT")]
        public long Id { get; set; }

        public string Name { get; set; }
        public string Colour { get; set; }
        public bool IsClosed { get; set; }

        public long Order { get; set; }
    }

    public class UpdateStatusResponse
    {
        public Status Status { get; set; }
    }
}