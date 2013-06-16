using System;
using System.Collections.Generic;
using System.Data;
using IssueTracker.Data;
using ServiceStack.OrmLite;

namespace IssueTracker.API.Repositories
{
    public interface ICommentChangeRepository : IRepository<CommentChange>
    {
        List<CommentChange> GetForComment(long commentId);
        List<CommentChange> GetForComment(Comment comment);
    }

    public class CommentChangeRepository : ICommentChangeRepository, IDisposable
    {
        private readonly IDbConnectionFactory dbFactory;
        private IDbConnection db;

        public CommentChangeRepository(IDbConnectionFactory dbFactory)
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


        public List<CommentChange> GetAll()
        {
            return Db.Select<CommentChange>();
        }

        public List<CommentChange> GetForComment(long commentId)
        {
            return Db.SelectParam<CommentChange>(x => x.CommentId == commentId);
        }
        public List<CommentChange> GetForComment(Comment comment)
        {
            return GetForComment(comment.Id);
        }

        public CommentChange GetById(long id)
        {
            return Db.IdOrDefault<CommentChange>(id);
        }

        public CommentChange Add(CommentChange status)
        {
            status.Id = 0;

            Db.Insert(status);
            status.Id = Db.GetLastInsertId();

            return status;
        }

        public void Update(CommentChange status)
        {
            Db.Update(status);
        }

        public void Delete(long id)
        {
            Db.DeleteById<CommentChange>(id);
        }

        public void Delete(CommentChange status)
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