using IssueTracker.API.Repositories;
using IssueTracker.API.Security;
using IssueTracker.API.Utilities;
using IssueTracker.Data.Users;
using ServiceStack.Common.Web;
using ServiceStack.Text;

namespace IssueTracker.API.Services
{
    internal class UsersService : BaseService
    {
        public UsersService(ISecurityService securityService)
            : base(securityService)
        {
        }

        public IUserRepository UserRepository { get; set; }

        public GetUserResponse Get(GetUser request)
        {
            var user = UserRepository.GetById(request.Id);
            if (user == null) throw HttpError.NotFound("user {0} not found".Fmt(request.Id));

            return new GetUserResponse
            {
                User = user.ToDto()
            };
        }
    }
}