using System;
using System.Collections.Generic;
using IssueTracker.API.Entities;
using ServiceStack.OrmLite;

namespace IssueTracker.API.Repositories
{
    public interface ICommentRepository : IRepository<CommentEntity>
    {
        List<CommentEntity> GetForIssue(long issueId);
    }

    internal class CommentRepository : BaseRepository, ICommentRepository
    {
        public CommentRepository(IDbConnectionFactory dbFactory)
            : base(dbFactory)
        {
        }

        public List<CommentEntity> GetAll()
        {
            throw new InvalidOperationException("Getting all comments is not a valid operation");

        }

        public List<CommentEntity> GetForIssue(long issueId)
        {
            return Db.SelectParam<CommentEntity>(x => x.IssueId == issueId);
        }

        public CommentEntity GetById(long id)
        {
            return Db.IdOrDefault<CommentEntity>(id);
        }

        public CommentEntity Add(CommentEntity comment)
        {
            comment.Id = 0;

            //var issueUpdated = issueRepository.SetUpdated(comment.IssueId);
            //if (!issueUpdated) return null;

            //comment.PersonId = personRepository.GetCurrent().Id;
            comment.CreatedAt = DateTime.UtcNow;
            comment.UpdatedAt = null;

            Db.Insert(comment);
            comment.Id = Db.GetLastInsertId();

            return comment;
        }

        public CommentEntity Update(CommentEntity comment)
        {
            var oldComment = GetById(comment.Id);
            if (oldComment == null) return null;

            //if (oldComment.PersonId != personRepository.GetCurrent().Id) return null;

            //var issueUpdated = issueRepository.SetUpdated(comment.IssueId);
            //if (!issueUpdated) return null;

            comment.PersonId = oldComment.PersonId;
            comment.CreatedAt = oldComment.CreatedAt;
            comment.UpdatedAt = DateTime.UtcNow;

            Db.Update(comment);

            return comment;
        }

        public bool Delete(long id)
        {
            var oldComment = GetById(id);
            if (oldComment == null) return false;

            //if (oldComment.PersonId != personRepository.GetCurrent().Id) return false;

            Db.DeleteById<CommentEntity>(id);

            return true;
        }
    }
}