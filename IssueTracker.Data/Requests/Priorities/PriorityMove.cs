using ServiceStack.ServiceHost;

namespace IssueTracker.Data.Requests.Priorities
{
    /// <summary>
    /// Move the given priority up by amount
    /// Amount can be negative to move down
    /// </summary>
    [Route("/priorities/{id}/move/{amount}", "POST")]
    public class PriorityMove
    {
        public long Id { get; set; }
        public long Amount { get; set; }
    }
}
