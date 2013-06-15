using System.Net;
using IssueTracker.API.Repositories;
using IssueTracker.Data;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.ServiceInterface;

namespace IssueTracker.API.Services.Comments
{
    public class CommentsService : Service
    {
        public CommentRepository CommentRepository { get; set; }

        /// <summary>
        /// Create a new comment
        /// </summary>
        public object Put(Comment request)
        {
            var comment = CommentRepository.Add(request);

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
                throw HttpError.NotFound("Comment does not exist: " + request.Id);
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
            var comment = CommentRepository.Delete(request);

            if (!comment)
            {
                throw HttpError.NotFound("Comment does not exist: " + request.Id);
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