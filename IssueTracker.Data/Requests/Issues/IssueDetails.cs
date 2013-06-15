using System.Collections.Generic;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface.ServiceModel;

namespace IssueTracker.Data.Requests.Issues
{
    [Route("/issues/{id}", "GET")]
    public class IssueDetails : IReturn<IssueDetailsResponse>
    {
        public int Id { get; set; }
    }

    public class IssueDetailsResponse : IHasResponseStatus
    {
        public IssueDetailsResponse()
        {
            ResponseStatus = new ResponseStatus();

            Comments = new List<Comment>();
        }

        public Issue Issue { get; set; }

        public Category Category { get; set; }
        public Status Status { get; set; }
        public Priority Priority { get; set; }

        public List<Comment> Comments { get; set; }

        public ResponseStatus ResponseStatus { get; set; }
    }

}
