using System.Collections.Generic;
using IssueTracker.Data.Users;
using ServiceStack.ServiceHost;

namespace IssueTracker.Data.Comments
{
    [Route("/issues/{issueId}/comments/{id}", "GET")]
    public class GetComment : IReturn<GetCommentResponse>
    {
        public long Id { get; set; }
        public long IssueId { get; set; }    
    }

    public class GetCommentResponse
    {
        public Comment Comment { get; set; }
        public List<CommentChange> Changes { get; set; }
    }
}
