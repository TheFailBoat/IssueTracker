using System.Collections.Generic;
using ServiceStack.ServiceHost;

namespace IssueTracker.Data.Issues.Statuses
{
    [Route("/statuses", "GET,OPTIONS")]
    public class ListStatuses : IReturn<ListStatusesResponse>
    {
    }

    public class ListStatusesResponse
    {
        public List<Status> Statuses { get; set; }
    }
}
