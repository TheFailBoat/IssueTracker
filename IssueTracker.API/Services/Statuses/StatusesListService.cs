using System.Collections.Generic;
using IssueTracker.API.Repositories;
using IssueTracker.Data;
using IssueTracker.Data.Requests.Statuses;
using ServiceStack.ServiceInterface;

namespace IssueTracker.API.Services.Statuses
{
    public class StatusesListService : Service
    {
        public IStatusRepository StatusRepository { get; set; }

        public List<Status> Get(StatusesList request)
        {
            return StatusRepository.GetAll();
        }
    }
}