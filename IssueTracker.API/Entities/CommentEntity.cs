using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IssueTracker.Data.Issues;
using ServiceStack.DataAnnotations;

namespace IssueTracker.API.Entities
{
    public class CommentEntity
    {
        [AutoIncrement]
        public long Id { get; set; }

        [References(typeof(IssueEntity))]
        public long IssueId { get; set; }
        [References(typeof(UserEntity))]
        public long PersonId { get; set; }

        public string Message { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}