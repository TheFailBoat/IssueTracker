using ServiceStack.ServiceHost;

namespace IssueTracker.Data.Requests
{
    [Route("/me")]
    public class AuthCheck : IReturn<Person>
    {
    }
}
