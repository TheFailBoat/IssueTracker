using IssueTracker.Data.Requests.Issues;
using ServiceStack.Common.Web;
using ServiceStack.ServiceInterface;

namespace IssueTracker.API.Services.Issues
{
    public class IssueDetailsService : Service
    {
        public IssueDetailsResponse Get(IssueDetails request)
        {
            var issue = DummyIssues.Get(request.Id);
            if (issue == null)
                throw HttpError.NotFound("Issue does not exist: " + request.Id);

            return new IssueDetailsResponse
            {
                Issue = issue
            };
        }
    }
}