using System;
using System.Collections.Generic;
using System.Linq;
using IssueTracker.API.Entities;
using IssueTracker.API.Security.Attributes.Internal;
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

        [RequirePermission(RequiresAdmin = true, RequiresMod = true)]
        public UserEntity Add(UserEntity item)
        {
            throw new NotImplementedException();
        }

        //TODO RequirePermission OR IsSelf
        public UserEntity Update(UserEntity item)
        {
            throw new NotImplementedException();
        }

        [RequirePermission(RequiresAdmin = true, RequiresMod = true)]
        public bool Delete(UserEntity item)
        {
            throw new NotImplementedException();
        }
    }
}