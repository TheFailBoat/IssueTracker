using ServiceStack.DataAnnotations;

namespace IssueTracker.Data
{
    // This entity isn't final (and probably shouldn't be used yet)
    public class CommentChange
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
