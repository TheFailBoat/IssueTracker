namespace IssueTracker.Data
{
    // This entity isn't final (and probably shouldn't be used yet)
    public class CommentChange
    {
        public int Id { get; set; }

        public int ColumnId { get; set; }
        public string Column { get; set; }

        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }
}
