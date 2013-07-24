using ServiceStack.ServiceHost;

namespace IssueTracker.Data.Issues.Statuses
{
    [Route("/statuses/{id}", "GET")]
    public class GetStatus : IReturn<GetStatusResponse>
    {
        [ApiMember]
        public long Id { get; set; }
    }

    public class GetStatusResponse
    {
        public Status Status { get; set; }
    }
}
