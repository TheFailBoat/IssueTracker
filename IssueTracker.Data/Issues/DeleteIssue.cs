using ServiceStack.ServiceHost;

namespace IssueTracker.Data.Issues
{
    [Route("/issues/{id}", "DELETE")]
    public class DeleteIssue : IReturn<DeleteIssueResponse>
    {
        [ApiMember]
        public long Id { get; set; }
    }

    public class DeleteIssueResponse
    {
        public bool Success { get; set; }
    }
}