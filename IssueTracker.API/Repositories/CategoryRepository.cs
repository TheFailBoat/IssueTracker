﻿using System;
using System.Collections.Generic;
using System.Data;
using IssueTracker.Data;
using ServiceStack.OrmLite;

namespace IssueTracker.API.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {

    }

    internal class CategoryRepository : ICategoryRepository, IDisposable
    {
        private readonly IDbConnectionFactory dbFactory;
        private IDbConnection db;

        public CategoryRepository(IDbConnectionFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        private IDbConnection Db
        {
            get
            {
                return db ?? (db = dbFactory.Open());
            }
        }


        public List<Category> GetAll()
        {
            return Db.Select<Category>();
        }

        public Category GetById(long id)
        {
            return Db.IdOrDefault<Category>(id);
        }

        public Category Add(Category status)
        {
            status.Id = 0;

            Db.Insert(status);
            status.Id = Db.GetLastInsertId();

            return status;
        }

        public void Update(Category status)
        {
            Db.Update(status);
        }

        public void Delete(long id)
        {
            Db.DeleteById<Category>(id);
        }

        public void Delete(Category status)
        {
            Delete(status.Id);
        }

        public void Dispose()
        {
            if (db != null)
                db.Dispose();
        }
    }
}