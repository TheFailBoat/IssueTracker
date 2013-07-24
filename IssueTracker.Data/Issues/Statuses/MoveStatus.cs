using ServiceStack.ServiceHost;

namespace IssueTracker.Data.Issues.Statuses
{
    /// <summary>
    /// Move the given priority up by amount
    /// Amount can be negative to move down
    /// </summary>
    [Route("/statuses/{id}/move/{amount}", "POST")]
    public class MoveStatus : IReturn<MoveStatusResponse>
    {
        [ApiMember]
        public long Id { get; set; }
        [ApiMember]
        public long Amount { get; set; }
    }

    public class MoveStatusResponse
    {
        public Status Status { get; set; }
    }
}
