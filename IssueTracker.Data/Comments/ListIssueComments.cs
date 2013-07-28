using System.Collections.Generic;
using ServiceStack.ServiceHost;

namespace IssueTracker.Data.Comments
{
    [Route("/issues/{issueId}/comments", "GET,OPTIONS")]
    public class ListIssueComments : IReturn<ListIssueCommentsResponse>
    {
        [ApiMember]
        public long IssueId { get; set; }
    }

    public class ListIssueCommentsResponse
    {
        public List<Comment> Comments { get; set; }
    }

    [Route("/comments", "GET,OPTIONS")]
    public class ListComments : IReturn<ListCommentsResponse>
    {
        [ApiMember(ParameterType="query")]
        public List<long> Ids { get; set; }
    }

    public class ListCommentsResponse
    {
        public List<Comment> Comments { get; set; }
    }
}
