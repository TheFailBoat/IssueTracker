namespace IssueTracker.Data
{
    public class Person
    {
        public int Id { get; set; }

        public int? CustomerId { get; set; }

        public string Name { get; set; }
        // This isn't a public field
        // public string Password { get; set; }

        public bool IsEmployee { get; set; }
    }
}
