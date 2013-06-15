using System;
using System.Collections.Generic;
using System.Data;
using IssueTracker.Data;
using ServiceStack.OrmLite;

namespace IssueTracker.API.Repositories
{
    public interface IStatusRepository : IRepository<Status>
    {

    }

    public class StatusRepository : IStatusRepository, IDisposable
    {
        public IDbConnectionFactory DbFactory { get; set; }
        private IDbConnection db;
        private IDbConnection Db
        {
            get
            {
                return db ?? (db = DbFactory.Open());
            }
        }


        public List<Status> GetAll()
        {
            return Db.Select<Status>();
        }

        public Status GetById(long id)
        {
            return Db.IdOrDefault<Status>(id);
        }

        public Status Add(Status status)
        {
            status.Id = 0;

            Db.Insert(status);
            status.Id = Db.GetLastInsertId();

            return status;
        }

        public Status Update(Status status)
        {
            Db.Update(status); 

            return status;
        }

        public bool Delete(long id)
        {
            Db.DeleteById<Status>(id);

            return true;
        }

        public bool Delete(Status status)
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