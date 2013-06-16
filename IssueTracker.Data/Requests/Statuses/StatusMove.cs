using ServiceStack.ServiceHost;

namespace IssueTracker.Data.Requests.Statuses
{
    /// <summary>
    /// Move the given priority up by amount
    /// Amount can be negative to move down
    /// </summary>
    [Route("/statuses/{id}/move/{amount}", "POST")]
    public class StatusMove
    {
        public long Id { get; set; }
        public long Amount { get; set; }
    }
}
