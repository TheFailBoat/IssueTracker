using ServiceStack.ServiceHost;

namespace IssueTracker.Data.Users
{
    [Route("/user/{id}", "GET")]
    public class GetUser : IReturn<User>
    {
        [ApiMember]
        public long Id { get; set; }
    }
}
