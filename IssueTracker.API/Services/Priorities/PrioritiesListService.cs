using System.Collections.Generic;
using IssueTracker.API.Repositories;
using IssueTracker.Data;
using IssueTracker.Data.Requests.Priorities;
using ServiceStack.ServiceInterface;

namespace IssueTracker.API.Services.Priorities
{
    [Authenticate]
    public class PrioritiesListService : Service
    {
        public IPriorityRepository PriorityRepository { get; set; }

        public List<Priority> Get(PrioritiesList request)
        {
            return PriorityRepository.GetAll();
        }
    }
}