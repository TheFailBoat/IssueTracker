﻿using IssueTracker.API.Repositories;
using IssueTracker.Data;
using IssueTracker.Data.Requests.Categories;
using ServiceStack.Common.Web;
using ServiceStack.ServiceInterface;

namespace IssueTracker.API.Services.Categories
{
    [Authenticate]
    public class CategoryDetailsService : Service
    {
        public ICategoryRepository CategoryRepository { get; set; }

        public Category Get(CategoryDetails request)
        {
            var category = CategoryRepository.GetById(request.Id);
            if (category == null)
                throw HttpError.NotFound("Category does not exist: " + request.Id);

            return category;
        }
    }
}