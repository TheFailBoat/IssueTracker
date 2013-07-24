using ServiceStack.DataAnnotations;

namespace IssueTracker.API.Entities
{
    [Alias("Priorities")]
    public class PriorityEntity
    {
        [AutoIncrement]
        public long Id { get; set; }

        public string Name { get; set; }
        public string Colour { get; set; }

        public long Order { get; set; }
    }
}