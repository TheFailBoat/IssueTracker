namespace IssueTracker.Data
{
    /// <summary>
    /// This is a purely virtual entity
    /// The actual data is stored as a UserAuth (provided by ServiceStack)
    /// </summary>
    public class Person
    {
        public long Id { get; set; }

        public long? CustomerId { get; set; }

        public string Name { get; set; }

        public bool IsEmployee { get; set; }
    }
}
