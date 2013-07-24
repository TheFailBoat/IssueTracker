using IssueTracker.Data;
using IssueTracker.Data.Comments;
using ServiceStack.DataAnnotations;

namespace IssueTracker.API.Entities
{
    public class CommentChangeEntity
    {
        [AutoIncrement]
        public long Id { get; set; }

        [References(typeof(Comment))]
        public long CommentId { get; set; }
        public string Column { get; set; }

        public object OldValue { get; set; }
        public object NewValue { get; set; }
    }
}