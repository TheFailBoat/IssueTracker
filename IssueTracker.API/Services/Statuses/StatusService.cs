using System.Net;
using IssueTracker.API.Repositories;
using IssueTracker.Data;
using IssueTracker.Data.Requests.Statuses;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.ServiceInterface;

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
            StatusRepository.Update(request);

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
        /// Delete an existing Status
        /// </summary>
        [RequiredPermission(Global.Constants.EmployeeRoleName)]
        public object Delete(Status request)
        {
            StatusRepository.Delete(request);

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