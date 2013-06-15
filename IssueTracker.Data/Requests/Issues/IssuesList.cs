using System.Collections.Generic;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface.ServiceModel;

namespace IssueTracker.Data.Requests.Issues
{
    [Route("/issues")]
    public class IssuesList
    {
    }

    public class IssuesListResponse : IHasResponseStatus
    {
        public IssuesListResponse()
        {
            ResponseStatus = new ResponseStatus();

            Issues = new List<Issue>();
        }

        public List<Issue> Issues { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }
}
