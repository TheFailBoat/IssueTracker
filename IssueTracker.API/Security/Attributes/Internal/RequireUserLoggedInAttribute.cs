using System.Security;

namespace IssueTracker.API.Security.Attributes.Internal
{
    public class RequireUserLoggedInAttribute : MethodInterceptAttribute
    {
        public override void Process(MethodInterceptArguments args)
        {
            var sec = args.Container.Resolve<ISecurityService>();

            if (sec.GetCurrentUser() != null) return;

            args.Cancel = true;
            throw new SecurityException("you must be logged in to do this");
        }
    }

    public class RequirePermissionAttribute : RequireUserLoggedInAttribute
    {
        public bool RequiresMod { get; set; }
        public bool RequiresAdmin { get; set; }

        public override void Process(MethodInterceptArguments args)
        {
            base.Process(args);

            var sec = args.Container.Resolve<ISecurityService>();

            var user = sec.GetCurrentUser();

            if (RequiresAdmin && !user.IsAdmin)
            {
                args.Cancel = true;
                throw new SecurityException("you must be an admin to do this");
            }
            if (RequiresMod && !user.IsMod)
            {
                args.Cancel = true;
                throw new SecurityException("you must be a mod to do this");
            }
        }
    }
}