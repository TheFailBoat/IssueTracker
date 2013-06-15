using IssueTracker.API.Repositories;
using IssueTracker.Data.Requests.Issues;
using ServiceStack.ServiceInterface;

namespace IssueTracker.API.Services.Issues
{
    public class IssuesService : Service
    {
        public IssueRepository IssueRepository { get; set; }

        public IssuesListResponse Get(IssuesList request)
        {
            return new IssuesListResponse
            {
                Issues = IssueRepository.GetAll()
            };
        }
    }
}