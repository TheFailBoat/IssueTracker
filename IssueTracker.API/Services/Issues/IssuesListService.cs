using System.Collections.Generic;
using IssueTracker.API.Repositories;
using IssueTracker.Data;
using IssueTracker.Data.Requests.Issues;
using ServiceStack.ServiceInterface;

namespace IssueTracker.API.Services.Issues
{
    public class IssuesService : Service
    {
        public IssueRepository IssueRepository { get; set; }

        public List<Issue> Get(IssuesList request)
        {
            return IssueRepository.GetAll();
        }
    }
}