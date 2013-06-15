using IssueTracker.API.Repositories;
using IssueTracker.Data.Requests.Issues;
using ServiceStack.Common.Web;
using ServiceStack.ServiceInterface;

namespace IssueTracker.API.Services.Issues
{
    public class IssueDetailsService : Service
    {
        public IssueRepository IssueRepository { get; set; }
        public CommentRepository CommentRepository { get; set; }
        //public CategoryRepository CategoryRepository { get; set; }
        //public PriorityRepository PriorityRepository { get; set; }
        //public StatusRepository StatusRepository { get; set; }

        public IssueDetailsResponse Get(IssueDetails request)
        {
            var issue = IssueRepository.GetById(request.Id);
            if (issue == null)
                throw HttpError.NotFound("Issue does not exist: " + request.Id);

            var comments = CommentRepository.GetForIssue(issue);

            return new IssueDetailsResponse
            {
                Issue = issue,
                //TODO Category = category,
                Comments = comments,
                //TODO Priority = priority,
                //TODO Status = status,
            };
        }
    }
}