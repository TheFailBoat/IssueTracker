using IssueTracker.API.Repositories;
using IssueTracker.Data;
using IssueTracker.Data.Requests.Statuses;
using ServiceStack.Common.Web;
using ServiceStack.ServiceInterface;

namespace IssueTracker.API.Services.Statuses
{
    [Authenticate]
    public class StatusDetailsService : Service
    {
        public IStatusRepository StatusRepository { get; set; }

        public Status Get(StatusDetails request)
        {
            var status = StatusRepository.GetById(request.Id);
            if (status == null)
                throw HttpError.NotFound("Status does not exist: " + request.Id);

            return status;
        }
    }
}