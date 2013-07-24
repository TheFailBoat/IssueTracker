using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IssueTracker.API.Entities;
using ServiceStack.OrmLite;

namespace IssueTracker.API.Repositories
{
    public interface IUserRepository : IRepository<UserEntity>
    {
        UserEntity GetByName(string username);
    }

    internal class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IDbConnectionFactory dbFactory)
            : base(dbFactory)
        {
        }

        public List<UserEntity> GetAll()
        {
            return Db.Select<UserEntity>();
        }

        public UserEntity GetById(long id)
        {
            return Db.IdOrDefault<UserEntity>(id);
        }

        public UserEntity GetByName(string username)
        {
            return Db.Select<UserEntity>(x => x.Username == username).SingleOrDefault();
        }

        public UserEntity Add(UserEntity item)
        {
            throw new NotImplementedException();
        }

        public UserEntity Update(UserEntity item)
        {
            throw new NotImplementedException();
        }

        public bool Delete(long id)
        {
            throw new NotImplementedException();
        }
    }
}