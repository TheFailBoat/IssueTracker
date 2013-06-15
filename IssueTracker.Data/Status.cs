namespace IssueTracker.Data
{
    public class Status
    {
        public long Id { get; set; }

        public string Name { get; set; }
        public string Colour { get; set; }
        public bool IsClosed { get; set; }

        public int Order { get; set; }
    }
}
