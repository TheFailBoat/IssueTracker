using System;
using IssueTracker.API.Security.Attributes.Internal;
using ServiceStack.DataAnnotations;

namespace IssueTracker.API.Entities
{
    [Alias("Issues")]
    [IssueReturnSecurity]
    public class IssueEntity
    {
        [AutoIncrement]
        public long Id { get; set; }

        [References(typeof(CustomerEntity))]
        public long? CustomerId { get; set; }
        [References(typeof(UserEntity))]
        public long ReporterId { get; set; }
        [References(typeof(CategoryEntity))]
        public long CategoryId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public int Progress { get; set; }
        [References(typeof(StatusEntity))]
        public long StatusId { get; set; }
        [References(typeof(PriorityEntity))]
        public long PriorityId { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}