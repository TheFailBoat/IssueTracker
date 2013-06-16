using System.Net;
using IssueTracker.API.Repositories;
using IssueTracker.Data;
using IssueTracker.Data.Requests.Statuses;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.ServiceInterface;

namespace IssueTracker.API.Services.Statuses
{
    public class StatusService : Service
    {
        public IStatusRepository StatusRepository { get; set; }

        /// <summary>
        /// Create a new Status
        /// </summary>
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
        public object Post(Status request)
        {
            var status = StatusRepository.Update(request);

            if (status == null)
            {
                throw HttpError.NotFound("Status does not exist: " + request.Id);
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
        public object Delete(Status request)
        {
            var status = StatusRepository.Delete(request);

            if (!status)
            {
                throw HttpError.NotFound("Status does not exist: " + request.Id);
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