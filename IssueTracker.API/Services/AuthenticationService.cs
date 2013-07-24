using IssueTracker.API.Entities;
using IssueTracker.API.Security;
using IssueTracker.API.Utilities;
using IssueTracker.Data.Authentication;
using ServiceStack.Common.Web;

namespace IssueTracker.API.Services
{
    internal class AuthenticationService : BaseService
    {
        public AuthenticationService(ISecurityService securityService)
            : base(securityService)
        {
        }

        public AuthLoginResponse Post(AuthLogin request)
        {
            UserEntity user;
            if (!SecurityService.TryAuthenticate(request.Username, request.Password, out user))
                throw HttpError.Unauthorized("invalid login");

            var token = SecurityService.IssueToken(user.Id);
            if (token == null)
                throw HttpError.Unauthorized("invalid login");

            return new AuthLoginResponse
            {
                AuthToken = token.Token,
                User = user.ToDto(),
                ExpiresAt = token.ExpiresAt
            };
        }

        public AuthCurrentResponse Get(AuthCurrent request)
        {
            return new AuthCurrentResponse
            {
                User = GetCurrentUser().ToDto()
            };
        }
    }
}