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

        public Issue Add(Issue comment)
        {
            comment.Id = 0;

            Db.Insert(comment);
            comment.Id = Db.GetLastInsertId();

            return comment;
        }

        public Issue Update(Issue comment)
        {
            Db.Update(comment); 

            return comment;
        }

        public bool Delete(long id)
        {
            Db.DeleteById<Issue>(id);

            return true;
        }

        public bool Delete(Issue comment)
        {
            return Delete(comment.Id);
        }

        public void Dispose()
        {
            if (db != null)
                db.Dispose();
        }
    }
}