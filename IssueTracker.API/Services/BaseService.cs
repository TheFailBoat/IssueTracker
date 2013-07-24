using IssueTracker.API.Entities;
using IssueTracker.API.Security;
using ServiceStack.ServiceInterface;

namespace IssueTracker.API.Services
{
    internal abstract class BaseService : Service
    {
        protected readonly ISecurityService SecurityService;

        protected BaseService(ISecurityService securityService)
        {
            SecurityService = securityService;
        }

        public UserEntity GetCurrentUser()
        {
            return SecurityService.GetCurrentUser();
        }
    }
}