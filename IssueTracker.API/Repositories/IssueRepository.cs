using System;
using System.Collections.Generic;
using System.Data;
using IssueTracker.Data;
using ServiceStack.OrmLite;

namespace IssueTracker.API.Repositories
{
    public interface IIssueRepository : IRepository<Issue>
    {

    }

    public class IssueRepository : IIssueRepository, IDisposable
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


        public List<Issue> GetAll()
        {
            return Db.Select<Issue>();
        }

        public Issue GetById(long id)
        {
            return Db.IdOrDefault<Issue>(id);
        }

        public Issue Add(Issue status)
        {
            status.Id = 0;

            Db.Insert(status);
            status.Id = Db.GetLastInsertId();

            return status;
        }

        public Issue Update(Issue status)
        {
            Db.Update(status); 

            return status;
        }

        public bool Delete(long id)
        {
            Db.DeleteById<Issue>(id);

            return true;
        }

        public bool Delete(Issue status)
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