using ServiceStack.DataAnnotations;

namespace IssueTracker.Data
{
    public class Category
    {
        [AutoIncrement]
        public long Id { get; set; }

        public string Name { get; set; }
        public string Colour { get; set; }
    }
}
