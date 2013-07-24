using ServiceStack.DataAnnotations;

namespace IssueTracker.API.Entities
{
    [Alias("Categories")]
    public class CategoryEntity
    {
        [AutoIncrement]
        public long Id { get; set; }

        public string Name { get; set; }
        public string Colour { get; set; }
    }
}