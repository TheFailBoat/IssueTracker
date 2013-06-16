using System.Net;
using IssueTracker.API.Repositories;
using IssueTracker.Data;
using IssueTracker.Data.Requests.Statuses;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.ServiceInterface;
using ServiceStack.Text;

namespace IssueTracker.API.Services.Statuses
{
    [Authenticate]
    public class StatusService : Service
    {
        public IStatusRepository StatusRepository { get; set; }

        /// <summary>
        /// Create a new Status
        /// </summary>
        [RequiredPermission(Global.Constants.EmployeeRoleName)]
        public object Put(Status request)
        {
            var status = StatusRepository.Add(request);

            if (status == null)
            {
                throw HttpError.Unauthorized("Creating a new status failed");
            }

            return new HttpResult(status)
            {
                StatusCode = HttpStatusCode.Created,
                Headers =
                {
                    { HttpHeaders.Location, Request.AbsoluteUri.CombineWith(status.Id) }
                }
            };
        }

        /// <summary>
        /// Update an existing Status
        /// </summary>
        [RequiredPermission(Global.Constants.EmployeeRoleName)]
        public object Post(Status request)
        {
            var status = StatusRepository.Update(request);

            if (status == null)
            {
                throw HttpError.Unauthorized("Updating status {0} failed".Fmt(request.Id));
            }

            return new HttpResult(status)
            {
                StatusCode = HttpStatusCode.NoContent,
                Headers =
                {
                    { HttpHeaders.Location, Request.AbsoluteUri.CombineWith(status.Id) }
                }
            };
        }

        /// <summary>
        /// Delete an existing Status
        /// </summary>
        [RequiredPermission(Global.Constants.EmployeeRoleName)]
        public object Delete(Status request)
        {
            var result = StatusRepository.Delete(request.Id);

            if (!result)
            {
                throw HttpError.Unauthorized("Deleting status {0} failed");
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

        /// <summary>
        /// Reorder the priorities
        /// </summary>
        [RequiredPermission(Global.Constants.EmployeeRoleName)]
        public object Post(StatusMove request)
        {
            StatusRepository.Move(request.Id, request.Amount);

            return new HttpResult
            {
                StatusCode = HttpStatusCode.NoContent,
                Headers =
                {
                    { HttpHeaders.Location, Request.AbsoluteUri.CombineWith(request.Id) }
                }
            };
        }
    }
}