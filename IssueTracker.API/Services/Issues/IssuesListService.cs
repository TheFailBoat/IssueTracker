using IssueTracker.Data.Requests.Issues;
using ServiceStack.ServiceInterface;

namespace IssueTracker.API.Services.Issues
{
    public class IssuesService : Service
    {
        public IssuesListResponse Get(IssuesList request)
        {
            return new IssuesListResponse
            {
                Issues = DummyIssues.GetAll()
            };
        }
    }
}