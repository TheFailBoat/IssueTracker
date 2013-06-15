using System.Collections.Generic;
using ServiceStack.ServiceHost;

namespace IssueTracker.Data.Requests.Issues
{
    [Route("/issues", "GET")]
    public class IssuesList : IReturn<List<Issue>>
    {
    }
}
