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

    internal class IssueRepository : IIssueRepository, IDisposable
    {
        private readonly IDbConnectionFactory dbFactory;
        private IDbConnection db;

        public IssueRepository(IDbConnectionFactory dbFactory)
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

        public void Update(Issue status)
        {
            Db.Update(status); 
        }

        public void Delete(long id)
        {
            Db.DeleteById<Issue>(id);
        }

        public void Delete(Issue status)
        {
            Delete(status.Id);
        }

        public void Dispose()
        {
            if (db != null)
                db.Dispose();
        }
    }
}