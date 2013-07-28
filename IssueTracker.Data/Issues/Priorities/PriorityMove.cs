using ServiceStack.ServiceHost;

namespace IssueTracker.Data.Issues.Priorities
{
    /// <summary>
    /// Move the given priority up by amount
    /// Amount can be negative to move down
    /// </summary>
    [Route("/priorities/{id}/move/{amount}", "POST,OPTIONS")]
    public class MovePriority : IReturn<MovePriorityResponse>
    {
        public long Id { get; set; }
        public long Amount { get; set; }
    }

    public class MovePriorityResponse
    {
        public Priority Priority { get; set; }
    }
}
