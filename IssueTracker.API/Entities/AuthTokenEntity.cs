using System;
using ServiceStack.DataAnnotations;

namespace IssueTracker.API.Entities
{
    [Alias("AuthToken")]
    public class AuthTokenEntity
    {
        [PrimaryKey]
        public string Id { get { return UserId + "-" + Token; } }

        public string Token { get; set; }

        [References(typeof(UserEntity))]
        public long UserId { get; set; }

        public DateTime IssuedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}