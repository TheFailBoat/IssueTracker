using System.Net;
using IssueTracker.API.Repositories;
using IssueTracker.Data;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.ServiceInterface;
using ServiceStack.Text;

namespace IssueTracker.API.Services.Comments
{
    [Authenticate]
    public class CommentsService : Service
    {
        public ICommentRepository CommentRepository { get; set; }

        /// <summary>
        /// Create a new comment
        /// </summary>
        public object Put(Comment request)
        {
            var comment = CommentRepository.Add(request);

            if (comment == null)
            {
                throw HttpError.Unauthorized("Creating a new comment failed");
            }

            return new HttpResult(comment)
            {
                StatusCode = HttpStatusCode.Created,
                Headers =
                {
                    { HttpHeaders.Location, Request.AbsoluteUri.CombineWith(comment.Id) }
                }
            };
        }

        /// <summary>
        /// Update an existing comment
        /// </summary>
        public object Post(Comment request)
        {
            var comment = CommentRepository.Update(request);

            if (comment == null)
            {
                throw HttpError.Unauthorized("Updating comment {0} failed".Fmt(request.Id));
            }

            return new HttpResult(comment)
            {
                StatusCode = HttpStatusCode.NoContent,
                Headers =
                {
                    { HttpHeaders.Location, Request.AbsoluteUri.CombineWith(comment.Id) }
                }
            };
        }

        /// <summary>
        /// Delete an existing comment
        /// </summary>
        public object Delete(Comment request)
        {
            var result = CommentRepository.Delete(request.Id);

            if (!result)
            {
                throw HttpError.Unauthorized("Deleting comment {0} failed".Fmt(request.Id));
            }

            return new HttpResult
            {
                StatusCode = HttpStatusCode.NoContent,
                Headers =
                {
                    { HttpHeaders.Location, Request.AbsoluteUri }
                }
            };
        }

    }
}