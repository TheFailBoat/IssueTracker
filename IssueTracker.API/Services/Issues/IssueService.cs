using System.Net;
using IssueTracker.API.Repositories;
using IssueTracker.Data;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.ServiceInterface;
using ServiceStack.Text;

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
        public object Post(Issue request)
        {
            var issue = IssueRepository.Add(request);

            if (issue == null)
            {
                throw HttpError.Unauthorized("Creating a new issue failed");
            }

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
        // [RequiredPermission(Global.Constants.EmployeeRoleName)]
        public object Put(Issue request)
        {
            var issue = IssueRepository.Update(request);

            if (issue == null)
            {
                throw HttpError.Unauthorized("Updating issue {0} failed".Fmt(request.Id));
            }

            return new HttpResult(issue)
            {
                StatusCode = HttpStatusCode.NoContent,
                Headers =
                {
                    { HttpHeaders.Location, Request.AbsoluteUri.CombineWith(issue.Id) }
                }
            };
        }

        /// <summary>
        /// Delete an existing issue
        /// </summary>
        [RequiredPermission(Global.Constants.EmployeeRoleName)]
        public object Delete(Issue request)
        {
            var result = IssueRepository.Delete(request.Id);

            if (!result)
            {
                throw HttpError.Unauthorized("Deleting issue {0} failed".Fmt(request.Id));
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