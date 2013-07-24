using System;
using System.Collections.Generic;
using System.Data;
using ServiceStack.OrmLite;

namespace IssueTracker.API.Repositories
{
    public abstract class BaseRepository : IDisposable
    {
        private readonly IDbConnectionFactory dbFactory;
        private IDbConnection db;

        protected BaseRepository(IDbConnectionFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        protected IDbConnection Db
        {
            get
            {
                return db ?? (db = dbFactory.Open());
            }
        }

        public void Dispose()
        {
            if (db != null)
                db.Dispose();
        }
    }

    public interface IRepository<T>
    {
        List<T> GetAll();
        T GetById(long id);

        T Add(T item);
        T Update(T item);
        bool Delete(long id);
    }
}