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
            throw new InvalidOperationException("Getting all changes is not a valid operation");
        }

        public List<CommentChange> GetForComment(long commentId)
        {
            return Db.SelectParam<CommentChange>(x => x.CommentId == commentId);
        }

        public CommentChange GetById(long id)
        {
            return Db.IdOrDefault<CommentChange>(id);
        }

        public CommentChange Add(CommentChange change)
        {
            Db.Insert(change);
            change.Id = Db.GetLastInsertId();

            return change;
        }

        public CommentChange Update(CommentChange change)
        {
            throw new InvalidOperationException("Updating changes is not a valid operation");
        }

        public bool Delete(CommentChange change)
        {
            throw new InvalidOperationException("Deleting changes is not a valid operation");
        }

        public void Dispose()
        {
            if (db != null)
                db.Dispose();
        }
    }
}