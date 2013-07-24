using ServiceStack.DataAnnotations;

namespace IssueTracker.API.Entities
{
    [Alias("Users")]
    public class UserEntity
    {
        [AutoIncrement]
        public long Id { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }

        [References(typeof(CustomerEntity))]
        public long? CustomerId { get; set; }

        /// <summary>
        /// Global Administrator
        /// </summary>
        public bool IsAdmin { get; set; }
        /// <summary>
        /// Local Administrator (for the Customer)
        /// </summary>
        public bool IsMod { get; set; }
    }
}