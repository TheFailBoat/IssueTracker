using System;
using System.Collections.Generic;
using System.Linq;
using IssueTracker.Data;

namespace IssueTracker.API.Services
{
    public class DummyIssues
    {
        private static List<Issue> _issues;

        private static void InitialiseIssues()
        {
            _issues = new List<Issue>
            {
                new Issue {Id = 1, Title = "Test Issue", Description = "This is the first issue", CreateAt = DateTime.UtcNow}, 
                new Issue {Id = 2, Title = "A monkey is attacking us", Description = "I think the title should be enough", CreateAt = DateTime.UtcNow}, 
                new Issue {Id = 3, Title = "The monitor in room 7 has died", Description = "The monitor in room 7 has stopped work on its own. It seems to be a bit wet though, might that have anything to do with it?", CreateAt = DateTime.UtcNow}, 
                new Issue {Id = 4, Title = "New Laptop", Description = "We need a new laptop for a new employee", CreateAt = DateTime.UtcNow}
            };
        }

        public static List<Issue> GetAll()
        {
            if (_issues == null)
            {
                InitialiseIssues();
            }

            return _issues.ToList();
        }

        public static Issue Get(int id)
        {
            if (_issues == null)
            {
                InitialiseIssues();
            }

            return _issues.SingleOrDefault(x => x.Id == id);
        }

        public static Issue Add(Issue issue)
        {
            if (_issues == null)
            {
                InitialiseIssues();
            }

            issue.Id = 0;
            _issues.Add(issue);
            issue.Id = _issues.Max(x => x.Id) + 1;

            return issue;
        }

        public static Issue Update(Issue issue)
        {
            if (_issues == null)
            {
                InitialiseIssues();
            }

            var existing = _issues.SingleOrDefault(x => x.Id == issue.Id);
            if (existing == null) return null;

            _issues.Remove(existing);
            _issues.Add(issue);

            return issue;
        }

        public static Issue Delete(Issue issue)
        {
            if (_issues == null)
            {
                InitialiseIssues();
            }

            var existing = _issues.SingleOrDefault(x => x.Id == issue.Id);
            if (existing == null) return null;

            _issues.Remove(existing);

            return issue;
        }
    }
}