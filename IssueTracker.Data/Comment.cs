using System;
using ServiceStack.DataAnnotations;
using ServiceStack.ServiceHost;

namespace IssueTracker.Data
{
    [Route("/issues/{issueId}/comments", "POST,PUT,DELETE")]
    public class Comment
    {
        [AutoIncrement]
        public long Id { get; set; }

        [References(typeof(Issue))]
        public long IssueId { get; set; }
        // UserAuth
        public long PersonId { get; set; }

        public string Message { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
