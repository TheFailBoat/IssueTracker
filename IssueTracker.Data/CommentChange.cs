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

        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }
}
