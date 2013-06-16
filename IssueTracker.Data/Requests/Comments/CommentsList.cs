using System.Collections.Generic;
using ServiceStack.ServiceHost;

namespace IssueTracker.Data.Requests.Comments
{
    [Route("/issues/{id}/comments", "GET")]
    public class CommentsList : IReturn<List<CommentDetailsResponse>>
    {
        public long Id { get; set; }
    }
}
