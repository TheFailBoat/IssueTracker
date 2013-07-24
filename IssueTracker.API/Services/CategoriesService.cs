using IssueTracker.API.Repositories;
using IssueTracker.API.Security;
using IssueTracker.API.Utilities;
using IssueTracker.Data.Issues.Categories;
using ServiceStack.Common.Web;
using ServiceStack.Text;

namespace IssueTracker.API.Services
{
    internal class CategoriesService : BaseService
    {
        public CategoriesService(ISecurityService securityService)
            : base(securityService)
        {
        }

        public ICategoryRepository CategoryRepository { get; set; }

        public ListCategoriesResponse Get(ListCategories request)
        {
            var categories = CategoryRepository.GetAll();

            return new ListCategoriesResponse
            {
                Categories = categories.ToDto()
            };
        }

        public GetCategoryResponse Get(GetCategory request)
        {
            var category = CategoryRepository.GetById(request.Id);
            if (category == null) throw HttpError.NotFound("category {0} not found".Fmt(request.Id));

            return new GetCategoryResponse
            {
                Category = category.ToDto()
            };
        }
    }
}