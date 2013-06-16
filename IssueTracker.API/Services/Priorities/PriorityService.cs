﻿using System.Net;
using IssueTracker.API.Repositories;
using IssueTracker.Data;
using IssueTracker.Data.Requests.Priorities;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.ServiceInterface;

namespace IssueTracker.API.Services.Priorities
{
    public class PriorityService : Service
    {
        public IPriorityRepository PriorityRepository { get; set; }

        /// <summary>
        /// Create a new Priority
        /// </summary>
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
        public object Post(Priority request)
        {
            var priority = PriorityRepository.Update(request);

            if (priority == null)
            {
                throw HttpError.NotFound("Priority does not exist: " + request.Id);
            }

            return new HttpResult(priority)
            {
                StatusCode = HttpStatusCode.NoContent,
                Headers =
                {
                    { HttpHeaders.Location, Request.AbsoluteUri.CombineWith(priority.Id) }
                }
            };
        }

        /// <summary>
        /// Delete an existing Priority
        /// </summary>
        public object Delete(Priority request)
        {
            var priority = PriorityRepository.Delete(request);

            if (!priority)
            {
                throw HttpError.NotFound("Priority does not exist: " + request.Id);
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