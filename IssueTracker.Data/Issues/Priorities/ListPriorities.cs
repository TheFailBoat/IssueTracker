using System.Collections.Generic;
using ServiceStack.ServiceHost;

namespace IssueTracker.Data.Issues.Priorities
{
    [Route("/priorities", "GET")]
    public class ListPriorities : IReturn<ListPrioritiesResponse>
    {
    }

    public class ListPrioritiesResponse
    {
        public List<Priority> Priorities { get; set; }
    }
}
