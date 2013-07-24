using IssueTracker.API.Repositories;
using IssueTracker.API.Security;
using IssueTracker.API.Utilities;
using IssueTracker.Data.Issues.Priorities;
using ServiceStack.Common.Web;
using ServiceStack.Text;

namespace IssueTracker.API.Services
{
    internal class PrioritiesService : BaseService
    {
        public PrioritiesService(ISecurityService securityService)
            : base(securityService)
        {
        }

        public IPriorityRepository PriorityRepository { get; set; }

        public ListPrioritiesResponse Get(ListPriorities request)
        {
            var priorities = PriorityRepository.GetAll();

            return new ListPrioritiesResponse
            {
                Priorities = priorities.ToDto()
            };
        }

        public GetPriorityResponse Get(GetPriority request)
        {
            var priority = PriorityRepository.GetById(request.Id);
            if (priority == null) throw HttpError.NotFound("priority {0} not found".Fmt(request.Id));

            return new GetPriorityResponse
            {
                Priority = priority.ToDto()
            };
        }
    }
}