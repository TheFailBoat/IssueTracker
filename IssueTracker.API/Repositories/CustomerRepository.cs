using System;
using System.Collections.Generic;
using System.Data;
using IssueTracker.API.Entities;
using IssueTracker.API.Security.Attributes.Internal;
using ServiceStack.OrmLite;

namespace IssueTracker.API.Repositories
{
    public interface ICustomerRepository : IRepository<CustomerEntity>
    {
    }

    internal class CustomerRepository : BaseRepository, ICustomerRepository
    {
        public CustomerRepository(IDbConnection db)
            : base(db)
        {
        }

        public List<CustomerEntity> GetAll()
        {
            return Db.Select<CustomerEntity>();
        }

        public CustomerEntity GetById(long id)
        {
            return Db.IdOrDefault<CustomerEntity>(id);
        }

        [RequirePermission(RequiresAdmin = true)]
        public CustomerEntity Add(CustomerEntity item)
        {
            throw new NotImplementedException();
        }

        [RequirePermission(RequiresMod = true)]
        public CustomerEntity Update(CustomerEntity item)
        {
            throw new NotImplementedException();
        }

        [RequirePermission(RequiresAdmin = true)]
        public bool Delete(CustomerEntity item)
        {
            throw new NotImplementedException();
        }
    }
}