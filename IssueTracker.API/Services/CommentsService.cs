using IssueTracker.API.Repositories;
using IssueTracker.API.Security;
using IssueTracker.API.Utilities;
using IssueTracker.Data.Comments;
using ServiceStack.Common.Web;
using ServiceStack.Text;

namespace IssueTracker.API.Services
{
    internal class CommentsService : BaseService
    {
        public CommentsService(ISecurityService securityService)
            : base(securityService)
        {
        }

        public ICommentRepository CommentRepository { get; set; }

        public ListCommentsResponse Get(ListComments request)
        {
            return new ListCommentsResponse
            {
                Comments = CommentRepository.GetAll().ToDto()
                //TODO Changes
            };
        }
        public GetCommentResponse Get(GetComment request)
        {
            var issue = CommentRepository.GetById(request.Id);
            if (issue == null) throw HttpError.NotFound("comment {0} not found".Fmt(request.Id));

            return new GetCommentResponse
            {
                Comment = issue.ToDto()
                //TODO Changes
            };
        }
    }
}