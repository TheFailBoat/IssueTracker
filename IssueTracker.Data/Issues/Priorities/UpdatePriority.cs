using ServiceStack.ServiceHost;

namespace IssueTracker.Data.Issues.Priorities
{
    [Route("/priorities", "POST")]
    [Route("/priorities/{id}", "PUT")]
    public class UpdatePriority : IReturn<UpdatePriorityResponse>
    {
        [ApiMember(Verb = "PUT")]
        public long Id { get; set; }

        public string Name { get; set; }
        public string Colour { get; set; }

        public long Order { get; set; }
    }

    public class UpdatePriorityResponse
    {
        public Priority Priority { get; set; }
    }
}