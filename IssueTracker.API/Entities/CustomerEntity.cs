using ServiceStack.DataAnnotations;

namespace IssueTracker.API.Entities
{
    [Alias("Customers")]
    public class CustomerEntity
    {
        [AutoIncrement]
        public long Id { get; set; }

        public string Name { get; set; }
        //TODO contact details
    }
}