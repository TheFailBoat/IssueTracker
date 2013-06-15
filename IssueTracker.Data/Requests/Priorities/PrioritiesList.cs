using System.Collections.Generic;
using ServiceStack.ServiceHost;

namespace IssueTracker.Data.Requests.Priorities
{
    [Route("/priorities", "GET")]
    public class PrioritiesList : IReturn<List<Priority>>
    {
    }
}
