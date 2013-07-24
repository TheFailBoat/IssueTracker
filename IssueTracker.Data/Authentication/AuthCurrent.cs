using IssueTracker.Data.Users;
using ServiceStack.ServiceHost;

namespace IssueTracker.Data.Authentication
{
    [Route("/auth/me", "GET")]
    public class AuthCurrent : IReturn<AuthCurrentResponse>
    {
    }

    public class AuthCurrentResponse
    {
        public User User { get; set; }
    }
}
