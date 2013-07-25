using System;
using IssueTracker.API.Entities;

namespace IssueTracker.API.Security
{
    interface ISecurityService
    {
        UserEntity GetUser(long id);
        UserEntity GetCurrentUser();

        bool TryAuthenticate(string username, string password, out UserEntity user);

        AuthTokenEntity IssueToken(long userId);
        AuthTokenEntity IssueToken(long userId, TimeSpan expiresIn);
        bool TryValidateToken(string token, out UserEntity user);

        void ResetPassword(UserEntity user, string password);
    }
}
