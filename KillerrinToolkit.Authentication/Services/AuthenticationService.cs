using KillerrinToolkit.Authentication.Models;
using KillerrinToolkit.Authentication.Models.Contracts;
using KillerrinToolkit.Authentication.Models.Enums;
using KillerrinToolkit.Core.Helpers;
using KillerrinToolkit.EFCore.Contracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KillerrinToolkit.Authentication.Services
{
    public class AuthenticationService
    {
        protected static Random _random = new Random();
        protected readonly IRepository<IUser> _userRepository;
        protected readonly IRepository<IAuthToken> _authTokenRepository;

        public AuthenticationService(IRepository<IUser> userRepository, IRepository<IAuthToken> authTokenRepository)
        {
            _userRepository = userRepository;
            _authTokenRepository = authTokenRepository;
        }

        protected int WorkFactor { get; set; } = 12;
        public virtual void FakeHash() { BCrypt.Net.BCrypt.HashPassword("", WorkFactor); }
        public virtual bool IsExpired(DateTime? utcExpiry)
        {
            if (utcExpiry == null) return false;
            return utcExpiry >= DateTime.UtcNow;
        }

        #region Users
        #endregion

        #region AuthToken
        public virtual bool VerifyAuthToken(string authToken, out IAuthToken authenticationToken)
        {
            authenticationToken = null;
            if (string.IsNullOrWhiteSpace(authToken)) return false;

            var token = _authTokenRepository.GetAllQuery()
                .Where(x => x.Token.Equals(authToken))
                .FirstOrDefault();

            authenticationToken = token;
            return token != null;
        }

        public virtual bool VerifyAuthTokenAndID(int userID, string authToken, out IAuthToken authenticationToken)
        {
            authenticationToken = null;
            if (string.IsNullOrWhiteSpace(authToken)) return false;

            var token = _authTokenRepository.GetAllQuery()
                .Where(x => x.UserId == userID)
                .Where(x => x.Token.Equals(authToken))
                .FirstOrDefault();

            authenticationToken = token;
            return token != null;
        }

        public virtual bool VerifyAuthTokenAndUsername(string username, string authToken, out IAuthToken authenticationToken)
        {
            authenticationToken = null;
            if (string.IsNullOrWhiteSpace(username)) return false;
            if (string.IsNullOrWhiteSpace(authToken)) return false;

            // Get the User
            var user = _userRepository.GetAllQuery().Where(x => x.Username.Equals(username)).FirstOrDefault();
            if (user == null)
                return false;

            return VerifyAuthTokenAndID(user.Id, authToken, out authenticationToken);
        }

        public virtual bool VerifyAuthTokenAndEmail(string email, string authToken, out IAuthToken authenticationToken)
        {
            authenticationToken = null;
            if (string.IsNullOrWhiteSpace(email)) return false;
            if (string.IsNullOrWhiteSpace(authToken)) return false;

            // Get the User
            var user = _userRepository.GetAllQuery().Where(x => x.Email.Equals(email)).FirstOrDefault();
            if (user == null)
                return false;

            return VerifyAuthTokenAndID(user.Id, authToken, out authenticationToken);
        }

        public virtual string GenerateAuthTokenString()
        {
            string auth = BCrypt.Net.BCrypt.HashString($"{_random.Next()}{Guid.NewGuid()}{_random.Next()}");
            return EncryptionHelper.EncodeBase64String(auth);
        }
        public virtual string GenerateAuthTokenString(int id, string username)
        {
            string auth = BCrypt.Net.BCrypt.HashString($"{_random.Next()}{id}{_random.Next()}{username}{_random.Next()}");
            return EncryptionHelper.EncodeBase64String(auth);
        }
        #endregion

        public virtual string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password, WorkFactor + _random.Next(0, 3));
        public virtual bool VerifyPassword(string password, string hash) => BCrypt.Net.BCrypt.Verify(password, hash);
    }
}
