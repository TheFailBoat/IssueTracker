﻿using System;

namespace IssueTracker.Data
{
    public class Issue
    {
        public long Id { get; set; }

        public long? CustomerId { get; set; }
        public long ReporterId { get; set; }
        public long CategoryId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public int Progress { get; set; }
        public long StatusId { get; set; }
        public long PriorityId { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
