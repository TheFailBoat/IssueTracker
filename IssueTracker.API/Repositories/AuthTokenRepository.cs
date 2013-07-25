using System;
using System.Collections.Generic;
using System.Linq;
using IssueTracker.API.Entities;
using ServiceStack.OrmLite;

namespace IssueTracker.API.Repositories
{
    public interface IAuthTokenRepository : IRepository<AuthTokenEntity>
    {
        AuthTokenEntity GetByToken(string token);
    }

    public class AuthTokenRepository : BaseRepository, IAuthTokenRepository
    {
        public AuthTokenRepository(IDbConnectionFactory dbFactory)
            : base(dbFactory)
        {
        }

        public List<AuthTokenEntity> GetAll()
        {
            return Db.Select<AuthTokenEntity>();
        }

        public AuthTokenEntity GetById(long id)
        {
            return Db.IdOrDefault<AuthTokenEntity>(id);
        }

        public AuthTokenEntity GetByToken(string token)
        {
            return Db.Select<AuthTokenEntity>(x => x.Token == token).SingleOrDefault();
        }

        public AuthTokenEntity Add(AuthTokenEntity item)
        {
            Db.Insert(item);

            return item;
        }

        public AuthTokenEntity Update(AuthTokenEntity item)
        {
            throw new InvalidOperationException();
        }

        public bool Delete(AuthTokenEntity item)
        {
            throw new InvalidOperationException();
        }
    }
}