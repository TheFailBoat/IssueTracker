using System;
using IssueTracker.API.Security.Attributes.Internal;
using ServiceStack.DataAnnotations;

namespace IssueTracker.API.Entities
{
    [Alias("Comments")]
    [CommentArgumentSecurity]
    [CommentReturnSecurity]
    public class CommentEntity
    {
        [AutoIncrement]
        public long Id { get; set; }

        [References(typeof(IssueEntity))]
        public long IssueId { get; set; }
        [References(typeof(UserEntity))]
        public long PersonId { get; set; }

        public string Message { get; set; }

        public bool Deleted { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}