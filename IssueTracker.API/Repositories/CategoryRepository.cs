using System;
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
        private readonly IPersonRepository personRepository;
        private IDbConnection db;

        public CategoryRepository(IDbConnectionFactory dbFactory, IPersonRepository personRepository)
        {
            this.dbFactory = dbFactory;
            this.personRepository = personRepository;
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

        public Category Add(Category category)
        {
            if (!personRepository.GetCurrent().IsEmployee)
            {
                return null;
            }

            category.Id = 0;

            Db.Insert(category);
            category.Id = Db.GetLastInsertId();

            return category;
        }

        public Category Update(Category category)
        {
            if (!personRepository.GetCurrent().IsEmployee)
            {
                return null;
            }

            Db.Update(category);

            return category;
        }

        public bool Delete(long id)
        {
            if (!personRepository.GetCurrent().IsEmployee)
            {
                return false;
            }

            Db.DeleteById<Category>(id);

            return true;
        }

        public void Dispose()
        {
            if (db != null)
                db.Dispose();
        }
    }
}