using ServiceStack.ServiceHost;

namespace IssueTracker.Data.Requests.Priorities
{
    [Route("/priorities/{id}", "GET")]
    public class PriorityDetails : IReturn<Priority>
    {
        public long Id { get; set; }
    }
}
