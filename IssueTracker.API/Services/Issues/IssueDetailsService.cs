using IssueTracker.API.Repositories;
using IssueTracker.Data.Requests.Issues;
using ServiceStack.Common.Web;
using ServiceStack.ServiceInterface;

namespace IssueTracker.API.Services.Issues
{
    [Authenticate]
    public class IssueDetailsService : Service
    {
        public IIssueRepository IssueRepository { get; set; }
        public ICategoryRepository CategoryRepository { get; set; }
        public IPersonRepository PersonRepository { get; set; }
        public IPriorityRepository PriorityRepository { get; set; }
        public IStatusRepository StatusRepository { get; set; }

        public IssueDetailsResponse Get(IssueDetails request)
        {
            var issue = IssueRepository.GetById(request.Id);
            if (issue == null)
                throw HttpError.NotFound("Issue does not exist: " + request.Id);

            var category = CategoryRepository.GetById(issue.CategoryId);
            var reporter = PersonRepository.GetById(issue.ReporterId);
            var priority = PriorityRepository.GetById(issue.PriorityId);
            var status = StatusRepository.GetById(issue.StatusId);

            return new IssueDetailsResponse
            {
                Issue = issue,
                Category = category,
                Reporter = reporter,
                Priority = priority,
                Status = status
            };
        }
    }
}