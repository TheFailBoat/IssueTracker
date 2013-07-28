using System;
using System.Collections.Generic;
using System.Data;
using IssueTracker.API.Entities;
using IssueTracker.API.Security.Attributes;
using ServiceStack.OrmLite;

namespace IssueTracker.API.Repositories
{
    public interface ICommentChangeRepository : IRepository<CommentChangeEntity>
    {
        [MethodType(MethodType.Get)]
        List<CommentChangeEntity> GetForComment(long commentId);
    }

    public class CommentChangeRepository : BaseRepository, ICommentChangeRepository
    {
        public CommentChangeRepository(IDbConnection db)
            : base(db)
        {
        }
        
        public List<CommentChangeEntity> GetAll()
        {
            throw new InvalidOperationException("Getting all changes is not a valid operation");
        }

        public List<CommentChangeEntity> GetForComment(long commentId)
        {
            return Db.SelectParam<CommentChangeEntity>(x => x.CommentId == commentId);
        }

        public CommentChangeEntity GetById(long id)
        {
            return Db.IdOrDefault<CommentChangeEntity>(id);
        }

        public CommentChangeEntity Add(CommentChangeEntity change)
        {
            Db.Insert(change);
            change.Id = Db.GetLastInsertId();

            return change;
        }

        public CommentChangeEntity Update(CommentChangeEntity change)
        {
            throw new InvalidOperationException("Updating changes is not a valid operation");
        }

        public bool Delete(CommentChangeEntity change)
        {
            throw new InvalidOperationException("Deleting changes is not a valid operation");
        }
    }
}