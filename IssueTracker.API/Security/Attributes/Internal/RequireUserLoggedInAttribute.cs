using System.Security;

namespace IssueTracker.API.Security.Attributes.Internal
{
    public class RequireUserLoggedInAttribute : MethodInterceptAttribute
    {
        public override void Process(MethodInterceptArguments args)
        {
            var sec = args.Container.Resolve<ISecurityService>();

            if (sec.GetCurrentUser() == null)
            {
                throw new SecurityException("you must be logged in to do this");
            }
        }
    }
}