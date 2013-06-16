using System.Collections.Generic;
using IssueTracker.Data.Requests.Comments;
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
        public Issue Issue { get; set; }

        public Category Category { get; set; }
        public Person Reporter { get; set; }
        public Status Status { get; set; }
        public Priority Priority { get; set; }
    }

}
