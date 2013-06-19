using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using IssueTracker.API.Utilities;
using IssueTracker.Data;
using ServiceStack.OrmLite;

namespace IssueTracker.API.Repositories
{
    public interface IIssueRepository : IRepository<Issue>
    {
        /// <summary>
        /// Same as <see cref="IIssueRepository.Update"/> but doesn't perform the authorization checks
        /// </summary>
        Issue UpdateInternal(Issue issue);

        /// <summary>
        /// Sets the UpdatedAt column to now
        /// </summary>
        bool SetUpdated(long id);
    }

    internal class IssueRepository : IIssueRepository, IDisposable
    {
        private readonly IDbConnectionFactory dbFactory;
        private readonly IPersonRepository personRepository;
        private IDbConnection db;

        public IssueRepository(IDbConnectionFactory dbFactory, IPersonRepository personRepository)
        {
            this.dbFactory = dbFactory;
            this.personRepository = personRepository;
        }

        private IDbConnection Db
        {
            get
            {
                return db ?? (db = dbFactory.Open());
            }
        }


        public List<Issue> GetAll()
        {
            var person = personRepository.GetCurrent();

            if (person.IsEmployee)
            {
                return Db.Select<Issue>();
            }

            if (person.CustomerId.HasValue)
            {
                return Db.Select<Issue>(i => i.CustomerId == person.CustomerId.Value || i.ReporterId == person.Id);
            }

            return Db.Select<Issue>(i => i.ReporterId == person.Id);
        }

        public Issue GetById(long id)
        {
            var issue = Db.IdOrDefault<Issue>(id);
            if (issue == null) return null;

            var person = personRepository.GetCurrent();

            if (person.IsEmployee || person.Id == issue.ReporterId || (person.CustomerId.HasValue && person.CustomerId == issue.CustomerId))
            {
                return issue;
            }

            return null;
        }

        public Issue Add(Issue issue)
        {
            var person = personRepository.GetCurrent();

            issue.Id = 0;
            issue.ReporterId = person.Id;
            issue.CreatedAt = DateTime.UtcNow;
            issue.UpdatedAt = null;

            if (!person.IsEmployee)
            {
                issue.CustomerId = person.CustomerId;
            }

            Db.Insert(issue);
            issue.Id = Db.GetLastInsertId();

            return issue;
        }

        public Issue Update(Issue issue)
        {
            var person = personRepository.GetCurrent();
            if (!person.IsEmployee) return null;

            return UpdateInternal(issue);
        }

        public Issue UpdateInternal(Issue issue)
        {
            var oldIssue = GetById(issue.Id);
            if (oldIssue == null) return null;

            issue.ReporterId = oldIssue.ReporterId;
            issue.CreatedAt = oldIssue.CreatedAt;
            issue.UpdatedAt = DateTime.UtcNow;

            Db.Update(issue);

            var changes = ChangeDetector.Diff(oldIssue, issue);
            if (changes.Any(x => x.Column == "UpdatedAt")) changes.Remove(changes.Single(x => x.Column == "UpdatedAt"));

            if (changes.Any())
            {
                Db.Insert(new Comment
                              {
                                  Message = "",
                                  IssueId = issue.Id,
                                  PersonId = personRepository.GetCurrent().Id,
                                  CreatedAt = DateTime.UtcNow
                              });
                var commentId = Db.GetLastInsertId();

                foreach (var change in changes)
                {
                    change.CommentId = commentId;
                    Db.Insert(change);
                }
            }

            return issue;
        }

        public bool SetUpdated(long id)
        {
            var issue = GetById(id);
            if (issue == null) return false;

            issue.UpdatedAt = DateTime.UtcNow;

            Db.Update(issue);

            return true;
        }

        public bool Delete(long id)
        {
            var oldIssue = GetById(id);
            if (oldIssue == null) return false;

            var person = personRepository.GetCurrent();
            if (!person.IsEmployee) return false;

            Db.DeleteById<Issue>(id);

            return true;
        }

        public void Dispose()
        {
            if (db != null)
                db.Dispose();
        }
    }
}