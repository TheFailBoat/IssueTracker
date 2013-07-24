using System.Collections.Generic;
using ServiceStack.ServiceHost;

namespace IssueTracker.Data.Comments
{
    [Route("/issues/{issueId}/comments", "GET")]
    public class ListComments : IReturn<ListCommentsResponse>
    {
        public long IssueId { get; set; }
    }

    public class ListCommentsResponse
    {
        public List<Comment> Comments { get; set; }
    }
}
