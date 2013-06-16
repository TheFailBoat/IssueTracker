using System.Net;
using IssueTracker.API.Repositories;
using IssueTracker.Data;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.ServiceInterface;

namespace IssueTracker.API.Services.Issues
{
    [Authenticate]
    public class IssueService : Service
    {
        public IIssueRepository IssueRepository { get; set; }
        public ICommentRepository CommentRepository { get; set; }
        public ICommentChangeRepository CommentChangeRepository { get; set; }

        /// <summary>
        /// Create a new issue
        /// </summary>
        public object Put(Issue request)
        {
            var issue = IssueRepository.Add(request);

            return new HttpResult(issue)
            {
                StatusCode = HttpStatusCode.Created,
                Headers =
                {
                    { HttpHeaders.Location, Request.AbsoluteUri.CombineWith(issue.Id) }
                }
            };
        }

        /// <summary>
        /// Update an existing issue
        /// </summary>
        [RequiredPermission(Global.Constants.EmployeeRoleName)]
        public object Post(Issue request)
        {
            IssueRepository.Update(request);

            //TODO this also creates a comment with commentchanges

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
        /// Delete an existing issue
        /// </summary>
        [RequiredPermission(Global.Constants.EmployeeRoleName)]
        public object Delete(Issue request)
        {
            IssueRepository.Delete(request);

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