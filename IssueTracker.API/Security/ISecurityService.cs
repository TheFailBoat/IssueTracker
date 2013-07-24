using System;
using IssueTracker.API.Entities;

namespace IssueTracker.API.Security
{
    interface ISecurityService
    {
        bool HasPermission(UserEntity user, string permission);
        bool AddPermission(UserEntity user, string permission);
        bool RemovePermission(UserEntity user, string permission);

        UserEntity GetUser(long id);
        UserEntity GetCurrentUser();

        bool TryAuthenticate(string username, string password, out UserEntity user);

        AuthTokenEntity IssueToken(long userId);
        AuthTokenEntity IssueToken(long userId, TimeSpan expiresIn);
        bool TryValidateToken(string token, out UserEntity user);

        void ResetPassword(UserEntity user, string password);
    }
}
