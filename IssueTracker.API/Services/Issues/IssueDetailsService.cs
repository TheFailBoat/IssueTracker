using IssueTracker.Data.Requests.Issues;
using ServiceStack.Common.Web;
using ServiceStack.ServiceInterface;

namespace IssueTracker.API.Services.Issues
{
    public class IssueDetailsService : Service
    {
        public IssueRepository IssueRepository { get; set; }
        public IssueDetailsResponse Get(IssueDetails request)
        {
            var issue = IssueRepository.GetById(request.Id);
            if (issue == null)
                throw HttpError.NotFound("Issue does not exist: " + request.Id);

            return new IssueDetailsResponse
            {
                Issue = issue
            };
        }
    }
}