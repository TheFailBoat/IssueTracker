using ServiceStack.ServiceHost;

namespace IssueTracker.Data.Issues
{
    [Route("/issues/{id}", "GET,OPTIONS")]
    public class GetIssue : IReturn<GetIssueResponse>
    {
        [ApiMember]
        public long Id { get; set; }
    }

    public class GetIssueResponse
    {
        public Issue Issue { get; set; }
    }
}