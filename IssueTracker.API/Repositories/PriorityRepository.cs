using System;
using System.Collections.Generic;
using System.Data;
using IssueTracker.Data;
using ServiceStack.OrmLite;

namespace IssueTracker.API.Repositories
{
    public interface IPriorityRepository : IRepository<Priority>
    {

    }

    internal class PriorityRepository : IPriorityRepository, IDisposable
    {
        private readonly IDbConnectionFactory dbFactory;
        private IDbConnection db;

        public PriorityRepository(IDbConnectionFactory dbFactory)
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


        public List<Priority> GetAll()
        {
            return Db.Select<Priority>();
        }

        public Priority GetById(long id)
        {
            return Db.IdOrDefault<Priority>(id);
        }

        public Priority Add(Priority status)
        {
            status.Id = 0;

            Db.Insert(status);
            status.Id = Db.GetLastInsertId();

            return status;
        }

        public Priority Update(Priority status)
        {
            Db.Update(status);

            return status;
        }

        public bool Delete(long id)
        {
            Db.DeleteById<Priority>(id);

            return true;
        }

        public bool Delete(Priority status)
        {
            return Delete(status.Id);
        }

        public void Dispose()
        {
            if (db != null)
                db.Dispose();
        }
    }
}