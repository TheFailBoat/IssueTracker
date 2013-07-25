using IssueTracker.API.Security.Attributes.Internal;
using ServiceStack.DataAnnotations;

namespace IssueTracker.API.Entities
{
    [Alias("CommentChanges")]
    [CommentChangeReturnSecurity]
    public class CommentChangeEntity
    {
        [AutoIncrement]
        public long Id { get; set; }

        [References(typeof(CommentEntity))]
        public long CommentId { get; set; }
        public string Column { get; set; }

        public object OldValue { get; set; }
        public object NewValue { get; set; }
    }
}