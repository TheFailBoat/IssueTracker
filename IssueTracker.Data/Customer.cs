using ServiceStack.DataAnnotations;

namespace IssueTracker.Data
{
    public class Customer
    {
        [AutoIncrement]
        public long Id { get; set; }

        public string Name { get; set; }
    }
}
