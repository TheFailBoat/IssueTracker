using System;

namespace IssueTracker.Data
{
    public class Comment
    {
        public long Id { get; set; }

        public long IssueId { get; set; }
        public long PersonId { get; set; }

        public string Message { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
