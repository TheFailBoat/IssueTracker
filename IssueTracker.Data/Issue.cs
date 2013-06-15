using System;
using ServiceStack.ServiceHost;

namespace IssueTracker.Data
{
    [Route("/issues", "POST,PUT,DELETE")]
    public class Issue
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public int ReporterId { get; set; }
        public int CategoryId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public int Progress { get; set; }
        public int StatusId { get; set; }
        public int PriorityId { get; set; }

        public DateTime CreateAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
