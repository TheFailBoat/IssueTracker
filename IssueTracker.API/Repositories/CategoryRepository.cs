using System.Collections.Generic;
using IssueTracker.API.Entities;
using ServiceStack.OrmLite;

namespace IssueTracker.API.Repositories
{
    public interface ICategoryRepository : IRepository<CategoryEntity>
    {

    }

    internal class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public CategoryRepository(IDbConnectionFactory dbFactory)
            : base(dbFactory)
        {
        }

        public List<CategoryEntity> GetAll()
        {
            return Db.Select<CategoryEntity>();
        }

        public CategoryEntity GetById(long id)
        {
            return Db.IdOrDefault<CategoryEntity>(id);
        }

        public CategoryEntity Add(CategoryEntity category)
        {
            category.Id = 0;

            Db.Insert(category);
            category.Id = Db.GetLastInsertId();

            return category;
        }

        public CategoryEntity Update(CategoryEntity category)
        {
            Db.Update(category);

            return category;
        }

        public bool Delete(long id)
        {
            Db.DeleteById<CategoryEntity>(id);

            return true;
        }
    }
}