using System;
using System.Collections.Generic;

namespace IssueTracker.Data
{
    public class Comment
    {
        public long Id { get; set; }

        public long IssueId { get; set; }
        public long PersonId { get; set; }

        public string Message { get; set; }

        public bool Deleted { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public List<CommentChange> Changes { get; set; }
    }
}
