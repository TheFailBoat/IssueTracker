using System;

namespace IssueTracker.Data
{
    public class Comment
    {
        public int Id { get; set; }

        public int IssueId { get; set; }
        public int PersonId { get; set; }

        public string Message { get; set; }

        public DateTime CreateAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
