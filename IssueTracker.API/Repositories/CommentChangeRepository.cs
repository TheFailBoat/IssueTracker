using System;
using System.Collections.Generic;
using System.Data;
using IssueTracker.Data;
using IssueTracker.Data.Comments;
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
        private readonly ICommentRepository commentRepository;
        private IDbConnection db;

        public CommentChangeRepository(IDbConnectionFactory dbFactory, ICommentRepository commentRepository)
        {
            this.dbFactory = dbFactory;
            this.commentRepository = commentRepository;
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
            var comment = commentRepository.GetById(commentId);
            if (comment == null) return new List<CommentChange>();

            return Db.SelectParam<CommentChange>(x => x.CommentId == commentId);
        }

        public CommentChange GetById(long id)
        {
            var change = Db.IdOrDefault<CommentChange>(id);
            if (change == null) return null;

            var comment = commentRepository.GetById(change.CommentId);
            if (comment == null) return null;

            return change;
        }

        public CommentChange Add(CommentChange change)
        {
            change.Id = 0;

            var comment = commentRepository.GetById(change.CommentId);
            if (comment == null) return null;

            Db.Insert(change);
            change.Id = Db.GetLastInsertId();

            return change;
        }

        public CommentChange Update(CommentChange change)
        {
            throw new InvalidOperationException("Updating changes is not a valid operation");
        }

        public bool Delete(long id)
        {
            var oldComment = GetById(id);
            if (oldComment == null) return false;

            var comment = commentRepository.GetById(oldComment.CommentId);
            if (comment == null) return false;

            Db.DeleteById<CommentChange>(id);

            return true;
        }

        public void Delete(CommentChange change)
        {
            Delete(change.Id);
        }

        public void Dispose()
        {
            if (db != null)
                db.Dispose();
        }
    }
}