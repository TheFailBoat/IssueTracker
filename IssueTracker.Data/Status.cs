﻿using ServiceStack.DataAnnotations;

namespace IssueTracker.Data
{
    public class Status
    {
        [AutoIncrement]
        public long Id { get; set; }

        public string Name { get; set; }
        public string Colour { get; set; }
        public bool IsClosed { get; set; }

        public long Order { get; set; }
    }
}
