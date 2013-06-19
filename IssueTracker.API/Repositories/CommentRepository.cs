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
    }

    internal class CommentRepository : ICommentRepository, IDisposable
    {
        private readonly IDbConnectionFactory dbFactory;
        private readonly IPersonRepository personRepository;
        private readonly IIssueRepository issueRepository;
        private IDbConnection db;

        public CommentRepository(IDbConnectionFactory dbFactory, IPersonRepository personRepository, IIssueRepository issueRepository)
        {
            this.dbFactory = dbFactory;
            this.personRepository = personRepository;
            this.issueRepository = issueRepository;
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
            throw new InvalidOperationException("Getting all comments is not a valid operation");

        }

        public List<Comment> GetForIssue(long issueId)
        {
            var issue = issueRepository.GetById(issueId);
            if (issue == null) return new List<Comment>();

            return Db.SelectParam<Comment>(x => x.IssueId == issueId);
        }

        public Comment GetById(long id)
        {
            var comment = Db.IdOrDefault<Comment>(id);
            if (comment == null) return null;

            var issue = issueRepository.GetById(comment.IssueId);
            if (issue == null) return null;

            return comment;
        }

        public Comment Add(Comment comment)
        {
            comment.Id = 0;

            var issueUpdated = issueRepository.SetUpdated(comment.IssueId);
            if (!issueUpdated) return null;

            comment.PersonId = personRepository.GetCurrent().Id;
            comment.CreatedAt = DateTime.UtcNow;
            comment.UpdatedAt = null;

            Db.Insert(comment);
            comment.Id = Db.GetLastInsertId();

            return comment;
        }

        public Comment Update(Comment comment)
        {
            var oldComment = GetById(comment.Id);
            if (oldComment == null) return null;

            if (oldComment.PersonId != personRepository.GetCurrent().Id) return null;

            var issueUpdated = issueRepository.SetUpdated(comment.IssueId);
            if (!issueUpdated) return null;

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

            if (oldComment.PersonId != personRepository.GetCurrent().Id) return false;

            Db.DeleteById<Comment>(id);

            return true;
        }

        public void Dispose()
        {
            if (db != null)
                db.Dispose();
        }
    }
}