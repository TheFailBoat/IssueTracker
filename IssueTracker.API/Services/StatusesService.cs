using IssueTracker.API.Repositories;
using IssueTracker.API.Security;
using IssueTracker.API.Utilities;
using IssueTracker.Data.Issues.Statuses;
using ServiceStack.Common.Web;
using ServiceStack.Text;

namespace IssueTracker.API.Services
{
    internal class StatusesService : BaseService
    {
        public StatusesService(ISecurityService securityService)
            : base(securityService)
        {
        }

        public IStatusRepository StatusRepository { get; set; }

        public ListStatusesResponse Get(ListStatuses request)
        {
            var statuses = StatusRepository.GetAll();

            return new ListStatusesResponse
            {
                Statuses = statuses.ToDto()
            };
        }

        public GetStatusResponse Get(GetStatus request)
        {
            var status = StatusRepository.GetById(request.Id);
            if (status == null) throw HttpError.NotFound("status {0} not found".Fmt(request.Id));

            return new GetStatusResponse
            {
                Status = status.ToDto()
            };
        }
    }
}