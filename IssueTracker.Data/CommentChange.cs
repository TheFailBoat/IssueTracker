namespace IssueTracker.Data
{
    public class CommentChange
    {
        public long Id { get; set; }

        public long CommentId { get; set; }
        public string Column { get; set; }

        public object OldValue { get; set; }
        public object NewValue { get; set; }
    }
}
