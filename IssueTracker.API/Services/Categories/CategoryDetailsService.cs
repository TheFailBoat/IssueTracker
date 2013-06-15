using IssueTracker.API.Repositories;
using IssueTracker.Data;
using IssueTracker.Data.Requests.Categories;
using ServiceStack.Common.Web;
using ServiceStack.ServiceInterface;

namespace IssueTracker.API.Services.Categories
{
    public class CategoryDetailsService : Service
    {
        public CategoryRepository CategoryRepository { get; set; }

        public Category Get(CategoryDetails request)
        {
            var category = CategoryRepository.GetById(request.Id);
            if (category == null)
                throw HttpError.NotFound("Category does not exist: " + request.Id);

            return category;
        }
    }
}