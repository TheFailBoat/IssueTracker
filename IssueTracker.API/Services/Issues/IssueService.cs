using System.Net;
using IssueTracker.Data;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.ServiceInterface;

namespace IssueTracker.API.Services.Issues
{
    public class IssueService : Service
    {
        /// <summary>
        /// Create a new issue
        /// </summary>
        public object Put(Issue request)
        {
            var issue = DummyIssues.Add(request);

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
        public object Post(Issue request)
        {
            var issue = DummyIssues.Update(request);

            if (issue == null)
            {
                throw HttpError.NotFound("Issue does not exist: " + request.Id);
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
        public object Delete(Issue request)
        {
            var issue = DummyIssues.Delete(request);

            if (issue == null)
            {
                throw HttpError.NotFound("Issue does not exist: " + request.Id);
            }

            return new HttpResult(issue)
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