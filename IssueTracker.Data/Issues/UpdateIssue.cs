using ServiceStack.ServiceHost;

namespace IssueTracker.Data.Issues
{
    [Route("/issues", "POST")]
    [Route("/issues/{id}", "PUT")]
    public class UpdateIssue : IReturn<UpdateIssueResponse>
    {
        [ApiMember(Verb = "PUT")]
        public long Id { get; set; }

        public long? CustomerId { get; set; }
        public long ReporterId { get; set; }
        public long CategoryId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public int Progress { get; set; }
        public long StatusId { get; set; }
        public long PriorityId { get; set; }
    }

    public class UpdateIssueResponse
    {
        public Issue Issue { get; set; }
    }
}