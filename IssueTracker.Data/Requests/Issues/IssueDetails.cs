using System.Collections.Generic;
using ServiceStack.ServiceHost;

namespace IssueTracker.Data.Requests.Issues
{
    [Route("/issues/{id}", "GET")]
    public class IssueDetails : IReturn<IssueDetailsResponse>
    {
        public long Id { get; set; }
    }

    public class IssueDetailsResponse
    {
        public IssueDetailsResponse()
        {
            Comments = new List<Comment>();
        }

        public Issue Issue { get; set; }

        public Category Category { get; set; }
        public Status Status { get; set; }
        public Priority Priority { get; set; }

        public List<Comment> Comments { get; set; }
    }

}
