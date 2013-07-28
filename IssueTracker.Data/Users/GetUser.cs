using ServiceStack.ServiceHost;

namespace IssueTracker.Data.Users
{
    [Route("/users/{id}", "GET,OPTIONS")]
    public class GetUser : IReturn<GetUserResponse>
    {
        [ApiMember]
        public long Id { get; set; }
    }

    public class GetUserResponse
    {
        public User User { get; set; }
    }
}
