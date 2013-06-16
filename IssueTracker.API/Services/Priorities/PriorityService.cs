using System.Net;
using IssueTracker.API.Repositories;
using IssueTracker.Data;
using IssueTracker.Data.Requests.Priorities;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.ServiceInterface;

namespace IssueTracker.API.Services.Priorities
{
    [Authenticate]
    public class PriorityService : Service
    {
        public IPriorityRepository PriorityRepository { get; set; }

        /// <summary>
        /// Create a new Priority
        /// </summary>
        [RequiredPermission(Global.Constants.EmployeeRoleName)]
        public object Put(Priority request)
        {
            var priority = PriorityRepository.Add(request);

            return new HttpResult(priority)
            {
                StatusCode = HttpStatusCode.Created,
                Headers =
                {
                    { HttpHeaders.Location, Request.AbsoluteUri.CombineWith(priority.Id) }
                }
            };
        }

        /// <summary>
        /// Update an existing Priority
        /// </summary>
        [RequiredPermission(Global.Constants.EmployeeRoleName)]
        public object Post(Priority request)
        {
            PriorityRepository.Update(request);

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
        /// Delete an existing Priority
        /// </summary>
        [RequiredPermission(Global.Constants.EmployeeRoleName)]
        public object Delete(Priority request)
        {
            PriorityRepository.Delete(request);

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
        public object Post(PriorityMove request)
        {
            PriorityRepository.Move(request.Id, request.Amount);

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