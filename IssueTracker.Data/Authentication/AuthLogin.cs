using System;
using IssueTracker.Data.Users;
using ServiceStack.ServiceHost;

namespace IssueTracker.Data.Authentication
{
    [Route("/auth/login", "POST")]
    public class AuthLogin : IReturn<AuthLoginResponse>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class AuthLoginResponse
    {
        public User User { get; set; }

        public string AuthToken { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
