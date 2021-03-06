﻿using System.Collections.Generic;
using IssueTracker.API.Repositories;
using IssueTracker.Data;
using IssueTracker.Data.Requests.Categories;
using ServiceStack.ServiceInterface;

namespace IssueTracker.API.Services.Categories
{
    [Authenticate]
    public class CategoriesListService : Service
    {
        public ICategoryRepository CategoryRepository { get; set; }

        public List<Category> Get(CategoriesList request)
        {
            return CategoryRepository.GetAll();
        }
    }
}