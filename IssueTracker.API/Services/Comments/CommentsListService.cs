using System.Collections.Generic;
using System.Linq;
using IssueTracker.API.Repositories;
using IssueTracker.Data;
using IssueTracker.Data.Requests.Comments;
using ServiceStack.Common.Web;
using ServiceStack.ServiceInterface;

namespace IssueTracker.API.Services.Comments
{
    public class CommentsListService : Service
    {
        public ICommentRepository CommentRepository { get; set; }
        public IPersonRepository PersonRepository { get; set; }
        public ICommentChangeRepository CommentChangeRepository { get; set; }
        public IIssueRepository IssueRepository { get; set; }

        public List<CommentDetailsResponse> Get(CommentsList request)
        {
            var issue = IssueRepository.GetById(request.Id);
            if (issue == null)
                throw HttpError.NotFound("Issue does not exist: " + request.Id);

            var comments = CommentRepository.GetForIssue(issue);

            return comments.Select(ToCommentDetails).ToList();
        }

        private CommentDetailsResponse ToCommentDetails(Comment comment)
        {
            var poster = PersonRepository.GetById(comment.PersonId);
            var changes = CommentChangeRepository.GetForComment(comment);

            return new CommentDetailsResponse
            {
                Comment = comment,
                Poster = poster,
                Changes = changes
            };
        }
    }
}