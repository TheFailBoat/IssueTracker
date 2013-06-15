using System.Collections.Generic;
using ServiceStack.ServiceHost;

namespace IssueTracker.Data.Requests.Statuses
{
    [Route("/statuses", "GET")]
    public class StatusesList : IReturn<List<Status>>
    {
    }
}
