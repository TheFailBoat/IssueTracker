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

    public class CommentRepository : ICommentRepository, IDisposable
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

        public Comment Add(Comment comment)
        {
            comment.Id = 0;

            Db.Insert(comment);
            comment.Id = Db.GetLastInsertId();

            return comment;
        }

        public Comment Update(Comment comment)
        {
            Db.Update(comment);

            return comment;
        }

        public bool Delete(long id)
        {
            Db.DeleteById<Comment>(id);

            return true;
        }

        public bool Delete(Comment comment)
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