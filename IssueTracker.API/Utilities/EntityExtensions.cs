using IssueTracker.API.Entities;
using IssueTracker.API.Repositories;
using IssueTracker.API.Security;

namespace IssueTracker.API.Utilities
{
    public static class EntityExtensions
    {
        public static bool CanUserAccess(this IssueEntity issue, UserEntity user)
        {
            if (issue == null || user == null) return false;

            if (user.IsAdmin) return true;

            if (user.IsMod)
            {
                return UserAndIssueMatch(issue, user);
            }

            return UserAndIssueMatch(issue, user) && !issue.Deleted;
        }

        /// <summary>
        /// Tests if an issue belongs to a user (or their customer)
        /// </summary>
        private static bool UserAndIssueMatch(IssueEntity issue, UserEntity user)
        {
            return (user.CustomerId != null && issue.CustomerId == user.CustomerId) || issue.ReporterId == user.Id;
        }

        public static bool CanUserAccess(this CommentEntity comment, UserEntity user, IInsecureRepository<IIssueRepository> issueRepository)
        {
            var issue = issueRepository.Repository.GetById(comment.IssueId);

            return issue.CanUserAccess(user);
        }
    }
}