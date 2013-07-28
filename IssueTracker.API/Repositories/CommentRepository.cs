using System;
using System.Collections.Generic;
using System.Data;
using IssueTracker.API.Entities;
using IssueTracker.API.Security.Attributes;
using IssueTracker.API.Security.Attributes.Internal;
using ServiceStack.OrmLite;

namespace IssueTracker.API.Repositories
{
    public interface ICommentRepository : IRepository<CommentEntity>
    {
        [MethodType(MethodType.Get)]
        List<CommentEntity> GetForIssue(long issueId);
    }

    internal class CommentRepository : BaseRepository, ICommentRepository
    {
        public CommentRepository(IDbConnection db)
            : base(db)
        {
        }

        [RequireUserLoggedIn]
        public List<CommentEntity> GetAll()
        {
            throw new InvalidOperationException("Getting all comments is not a valid operation");
        }

        [RequireUserLoggedIn]
        public List<CommentEntity> GetForIssue(long issueId)
        {
            return Db.SelectParam<CommentEntity>(x => x.IssueId == issueId);
        }

        [RequireUserLoggedIn]
        public CommentEntity GetById(long id)
        {
            return Db.IdOrDefault<CommentEntity>(id);
        }

        [RequireUserLoggedIn]
        public CommentEntity Add(CommentEntity comment)
        {
            Db.Insert(comment);
            comment.Id = Db.GetLastInsertId();

            return comment;
        }

        [RequireUserLoggedIn]
        public CommentEntity Update(CommentEntity comment)
        {
            Db.Update(comment);

            return comment;
        }

        [RequireUserLoggedIn]
        public bool Delete(CommentEntity comment)
        {
            comment.Deleted = true;
            Db.Update(comment);

            return true;
        }
    }
}