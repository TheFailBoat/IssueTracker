using ServiceStack.ServiceHost;

namespace IssueTracker.Data.Authentication
{
    [Route("/auth/permission/{permission}", "POST")]
    public class AuthPermissionTest : IReturn<AuthPermissionTestResponse>
    {
        public string Permission { get; set; }
    }

    public class AuthPermissionTestResponse
    {
        public bool Result { get; set; }
    }
}
