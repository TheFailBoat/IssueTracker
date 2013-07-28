using ServiceStack.ServiceHost;

namespace IssueTracker.Data.Issues.Priorities
{
    [Route("/priorities/{id}", "GET,OPTIONS")]
    public class GetPriority : IReturn<GetPriorityResponse>
    {
        [ApiMember]
        public long Id { get; set; }
    }

    public class GetPriorityResponse
    {
        public Priority Priority { get; set; }
    }
}