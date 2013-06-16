using System.Net;
using IssueTracker.API.Repositories;
using IssueTracker.Data;
using ServiceStack.Common;
using ServiceStack.Common.Web;
using ServiceStack.ServiceInterface;
using ServiceStack.Text;

namespace IssueTracker.API.Services.Categories
{
    [Authenticate]
    public class CategoryService : Service
    {
        public ICategoryRepository CategoryRepository { get; set; }

        /// <summary>
        /// Create a new Category
        /// </summary>
        [RequiredPermission(Global.Constants.EmployeeRoleName)]
        public object Put(Category request)
        {
            var category = CategoryRepository.Add(request);

            if (category == null)
            {
                throw HttpError.Unauthorized("Creating a new category failed");
            }

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
        [RequiredPermission(Global.Constants.EmployeeRoleName)]
        public object Post(Category request)
        {
            var category = CategoryRepository.Update(request);

            if (category == null)
            {
                throw HttpError.Unauthorized("Updating category {0} failed".Fmt(request.Id));
            }

            return new HttpResult(category)
            {
                StatusCode = HttpStatusCode.NoContent,
                Headers =
                {
                    { HttpHeaders.Location, Request.AbsoluteUri.CombineWith(category.Id) }
                }
            };
        }

        /// <summary>
        /// Delete an existing Category
        /// </summary>
        [RequiredPermission(Global.Constants.EmployeeRoleName)]
        public object Delete(Category request)
        {
            var result = CategoryRepository.Delete(request.Id);

            if (!result)
            {
                throw HttpError.Unauthorized("Deleting category {0} failed".Fmt(request.Id));
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