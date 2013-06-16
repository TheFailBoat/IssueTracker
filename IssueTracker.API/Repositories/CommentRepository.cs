using System;
using System.Collections.Generic;
using System.Data;
using IssueTracker.Data;
using ServiceStack.OrmLite;

namespace IssueTracker.API.Repositories
{
    public interface ICommentRepository : IRepository<Comment>
    {
        List<Comment> GetForIssue(long issueId);
        List<Comment> GetForIssue(Issue issue);
    }

    internal class CommentRepository : ICommentRepository, IDisposable
    {
        private readonly IDbConnectionFactory dbFactory;
        private IDbConnection db;

        public CommentRepository(IDbConnectionFactory dbFactory)
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


        public List<Comment> GetAll()
        {
            return Db.Select<Comment>();
        }

        public List<Comment> GetForIssue(long issueId)
        {
            return Db.SelectParam<Comment>(x => x.IssueId == issueId);
        }
        public List<Comment> GetForIssue(Issue issue)
        {
            return GetForIssue(issue.Id);
        }

        public Comment GetById(long id)
        {
            return Db.IdOrDefault<Comment>(id);
        }

        public Comment Add(Comment status)
        {
            status.Id = 0;

            Db.Insert(status);
            status.Id = Db.GetLastInsertId();

            return status;
        }

        public void Update(Comment status)
        {
            Db.Update(status);
        }

        public void Delete(long id)
        {
            Db.DeleteById<Comment>(id);
        }

        public void Delete(Comment status)
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