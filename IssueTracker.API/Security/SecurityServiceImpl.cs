using System;
using System.Web;
using IssueTracker.API.Entities;
using IssueTracker.API.Repositories;
using IssueTracker.API.Utilities;
using ServiceStack.Configuration;
using HttpRequestWrapper = ServiceStack.WebHost.Endpoints.Extensions.HttpRequestWrapper;

namespace IssueTracker.API.Security
{
    public class SecurityServiceImpl : ISecurityService
    {
        private static readonly double AuthTokenTimeout = new AppSettings().Get("AuthTokenTimeout", 0.5);
        private static readonly int TokenSize = new AppSettings().Get("AuthTokenLength", 48);

        private readonly IUserRepository userRepository;
        private readonly IAuthTokenRepository authTokenRepository;

        public SecurityServiceImpl(IUserRepository userRepository, IAuthTokenRepository authTokenRepository)
        {
            this.userRepository = userRepository;
            this.authTokenRepository = authTokenRepository;
        }

        public UserEntity GetUser(long id)
        {
            return userRepository.GetById(id);
        }

        private bool hasCachedUser;
        private UserEntity cachedUser;
        public UserEntity GetCurrentUser()
        {
            if (!hasCachedUser)
            {
                hasCachedUser = true;

                var auth = HttpUtils.GetAuthToken(new HttpRequestWrapper(HttpContext.Current.Request));
                if (auth == null) return null;

                UserEntity user;
                cachedUser = TryValidateToken(auth, out user) ? user : null;
            }

            return cachedUser;
        }

        public bool TryAuthenticate(string username, string password, out UserEntity user)
        {
            user = userRepository.GetByName(username);
            if (user == null) return false;

            // more checks?

            return PasswordHashingService.CheckPassword(user.Password, password);
        }

        public AuthTokenEntity IssueToken(long userId)
        {
            return IssueToken(userId, TimeSpan.FromHours(AuthTokenTimeout));
        }
        public AuthTokenEntity IssueToken(long userId, TimeSpan expiresIn)
        {
            var user = userRepository.GetById(userId);
            if (user == null) return null;

            var token = new AuthTokenEntity
            {
                Token = StringUtils.SecureRandom(TokenSize),
                IssuedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.Add(expiresIn),
                UserId = user.Id
            };

            authTokenRepository.Add(token);

            return token;
        }
        public bool TryValidateToken(string token, out UserEntity user)
        {
            if (token.Length != TokenSize)
            {
                user = null;
                return false;
            }

            var authToken = authTokenRepository.GetByToken(token);

            if (authToken == null)
            {
                user = null;
                return false;
            }

            if (authToken.ExpiresAt < DateTime.UtcNow)
            {
                user = null;
                return false;
            }

            user = GetUser(authToken.UserId);
            return true;
        }

        public void ResetPassword(UserEntity user, string password)
        {
            throw new NotImplementedException();
        }
    }
}