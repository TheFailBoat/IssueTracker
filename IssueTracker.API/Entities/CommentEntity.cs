using System;
using ServiceStack.DataAnnotations;

namespace IssueTracker.API.Entities
{
    [Alias("Comments")]
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