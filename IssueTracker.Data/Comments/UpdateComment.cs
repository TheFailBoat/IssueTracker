using ServiceStack.ServiceHost;

namespace IssueTracker.Data.Comments
{
    [Route("/issues/{issueId}/comments", "POST")]
    [Route("/issues/{issueId}/comments/{id}", "PUT")]
    public class UpdateComment : IReturn<UpdateCommentResponse>
    {
        public long Id { get; set; }
        public long IssueId { get; set; }

        public string Message { get; set; }
    }

    public class UpdateCommentResponse
    {
        public Comment Comment { get; set; }
    }
}