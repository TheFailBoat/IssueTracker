using ServiceStack.DataAnnotations;

namespace IssueTracker.API.Entities
{
    [Alias("Statuses")]
    public class StatusEntity
    {
        [AutoIncrement]
        public long Id { get; set; }

        public string Name { get; set; }
        public string Colour { get; set; }
        public bool IsClosed { get; set; }

        public long Order { get; set; }
    }
}