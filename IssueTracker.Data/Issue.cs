using System;
using ServiceStack.DataAnnotations;
using ServiceStack.ServiceHost;

namespace IssueTracker.Data
{
    [Route("/issues", "POST,PUT,DELETE")]
    public class Issue
    {
        [AutoIncrement]
        public long Id { get; set; }

        [References(typeof(Customer))]
        public long CustomerId { get; set; }
        // UserAuth
        public long ReporterId { get; set; }
        [References(typeof(Category))]
        public long CategoryId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public int Progress { get; set; }
        [References(typeof(Status))]
        public long StatusId { get; set; }
        [References(typeof(Priority))]
        public long PriorityId { get; set; }

        public DateTime CreateAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
