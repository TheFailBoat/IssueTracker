using System.Net;
using IssueTracker.API.Repositories;
using IssueTracker.Data;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.ServiceInterface;

namespace IssueTracker.API.Services.Categories
{
    public class CategoryService : Service
    {
        public ICategoryRepository CategoryRepository { get; set; }

        /// <summary>
        /// Create a new Category
        /// </summary>
        public object Put(Category request)
        {
            var category = CategoryRepository.Add(request);

            return new HttpResult(category)
            {
                StatusCode = HttpStatusCode.Created,
                Headers =
                {
                    { HttpHeaders.Location, Request.AbsoluteUri.CombineWith(category.Id) }
                }
            };
        }

        /// <summary>
        /// Update an existing Category
        /// </summary>
        public object Post(Category request)
        {
            CategoryRepository.Update(request);

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
        /// Delete an existing Category
        /// </summary>
        public object Delete(Category request)
        {
            CategoryRepository.Delete(request);

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