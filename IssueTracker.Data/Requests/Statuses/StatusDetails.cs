using ServiceStack.ServiceHost;

namespace IssueTracker.Data.Requests.Statuses
{
    [Route("/statuses/{id}", "GET")]
    public class StatusDetails : IReturn<Status>
    {
        public long Id { get; set; }
    }
}
