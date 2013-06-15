using IssueTracker.API.Repositories;
using IssueTracker.Data;
using IssueTracker.Data.Requests.Priorities;
using ServiceStack.Common.Web;
using ServiceStack.ServiceInterface;

namespace IssueTracker.API.Services.Priorities
{
    public class PriorityDetailsService : Service
    {
        public IPriorityRepository PriorityRepository { get; set; }

        public Priority Get(PriorityDetails request)
        {
            var priority = PriorityRepository.GetById(request.Id);
            if (priority == null)
                throw HttpError.NotFound("Priority does not exist: " + request.Id);

            return priority;
        }
    }
}