namespace IssueTracker.Data
{
    public class User
    {
        public long Id { get; set; }

        public long? CustomerId { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }

        public bool IsAdmin { get; set; }
        public bool IsMod { get; set; }
    }
}
