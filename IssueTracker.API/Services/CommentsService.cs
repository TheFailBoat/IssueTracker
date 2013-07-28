using System.Linq;
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

        public ListIssueCommentsResponse Get(ListIssueComments request)
        {
            return new ListIssueCommentsResponse
            {
                Comments = CommentRepository.GetForIssue(request.IssueId).ToDto()
            };
        }
        public ListCommentsResponse Get(ListComments request)
        {
            var ids = request.Ids ?? Request.QueryString["ids[]"].Split(',').Select(long.Parse);

            var changeRepository = ResolveService<IInsecureRepository<ICommentChangeRepository>>();

            return new ListCommentsResponse
            {
                Comments = ids.Select(x => CommentRepository.GetById(x).ToDto(changeRepository)).ToList()
            };
        }

        public GetCommentResponse Get(GetComment request)
        {
            var comment = CommentRepository.GetById(request.Id);
            if (comment == null) throw HttpError.NotFound("comment {0} not found".Fmt(request.Id));

            var changeRepository = ResolveService<IInsecureRepository<ICommentChangeRepository>>();

            return new GetCommentResponse
            {
                Comment = comment.ToDto(changeRepository)
            };
        }
    }
}