using ServiceStack.ServiceHost;

namespace IssueTracker.Data.Issues
{
    [Route("/issues/{id}", "GET")]
    public class GetIssue : IReturn<GetIssueResponse>
    {
        public long Id { get; set; }
    }

    public class GetIssueResponse
    {
        public Issue Issue { get; set; }
    }
}