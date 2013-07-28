using System.Collections.Generic;
using System.Data;
using IssueTracker.API.Entities;
using IssueTracker.API.Security.Attributes.Internal;
using ServiceStack.OrmLite;

namespace IssueTracker.API.Repositories
{
    public interface ICategoryRepository : IRepository<CategoryEntity>
    {

    }

    internal class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public CategoryRepository(IDbConnection db)
            : base(db)
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

        [RequirePermission(RequiresAdmin = true)]
        public CategoryEntity Add(CategoryEntity category)
        {
            category.Id = 0;

            Db.Insert(category);
            category.Id = Db.GetLastInsertId();

            return category;
        }

        [RequirePermission(RequiresAdmin = true)]
        public CategoryEntity Update(CategoryEntity category)
        {
            Db.Update(category);

            return category;
        }

        [RequirePermission(RequiresAdmin = true)]
        public bool Delete(CategoryEntity category)
        {
            Db.DeleteById<CategoryEntity>(category.Id);

            return true;
        }
    }
}