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
        public ICommentRepository CommentRepository { get; set; }

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
            CommentRepository.Update(request);

            return new HttpResult(request)
            {
                StatusCode = HttpStatusCode.NoContent,
                Headers =
                {
                    { HttpHeaders.Location, Request.AbsoluteUri.CombineWith(request.Id) }
                }
            };
        }

        /// <summary>
        /// Delete an existing comment
        /// </summary>
        public object Delete(Comment request)
        {
            CommentRepository.Delete(request);

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