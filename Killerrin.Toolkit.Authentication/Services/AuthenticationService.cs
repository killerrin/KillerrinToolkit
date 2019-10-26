using Killerrin.Toolkit.Authentication.Models;
using Killerrin.Toolkit.Authentication.Models.Contracts;
using Killerrin.Toolkit.Authentication.Models.Enums;
using Killerrin.Toolkit.Core.Helpers;
using Killerrin.Toolkit.EFCore.Contracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Killerrin.Toolkit.Authentication.Services
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

        /// <summary>
        /// Preforms a Fake Hash. This is used as a dummy call to make incorrect authentication take longer
        /// </summary>
        public virtual void FakeHash() { BCrypt.Net.BCrypt.HashPassword("", WorkFactor); }

        /// <summary>
        /// Checks if a password is expired
        /// </summary>
        /// <param name="utcExpiry">The UTC Expiry Date to check against Today</param>
        /// <returns></returns>
        public virtual bool IsExpired(DateTime? utcExpiry)
        {
            if (utcExpiry == null) return false;
            return utcExpiry <= DateTime.UtcNow;
        }

        #region Users
        #endregion

        #region AuthToken
        /// <summary>
        /// Checks if a Given Auth Token is valid
        /// </summary>
        /// <param name="authToken">The Auth Token string</param>
        /// <param name="authenticationToken">The Auth Token to validate</param>
        /// <returns>Whether the token is valid</returns>
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

        /// <summary>
        /// Checks if a Given Auth Token is valid for a given User using their Id
        /// </summary>
        /// <param name="userID">The UserId to validate against</param>
        /// <param name="authToken">The Auth Token string</param>
        /// <param name="authenticationToken">The Auth Token which will be returned</param>
        /// <returns>Whether the token is valid</returns>
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

        /// <summary>
        /// Checks if a Given Auth Token is valid for a given user using their Username
        /// </summary>
        /// <param name="username">The Username to validate against</param>
        /// <param name="authToken">The Auth Token string</param>
        /// <param name="authenticationToken">The Auth Token which will be returned</param>
        /// <returns>Whether the token is valid</returns>
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

        /// <summary>
        /// Checks if a Given Auth Token is valid for a given user using their Email
        /// </summary>
        /// <param name="email">The Email to validate against</param>
        /// <param name="authToken">The Auth Token string</param>
        /// <param name="authenticationToken">The Auth Token which will be returned</param>
        /// <returns>Whether the token is valid</returns>
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

        /// <summary>
        /// Generates an AuthToken String
        /// </summary>
        /// <returns>A new Auth Token String</returns>
        public virtual string GenerateAuthTokenString()
        {
            string auth = BCrypt.Net.BCrypt.HashString($"{_random.Next()}{Guid.NewGuid()}{_random.Next()}");
            return EncryptionHelper.EncodeBase64String(auth);
        }

        /// <summary>
        /// Generates an AuthToken String using an ID and Username as additional Salts
        /// </summary>
        /// <param name="id">The ID of a User</param>
        /// <param name="username">The Username of a User</param>
        /// <returns>A new Auth Token String</returns>
        public virtual string GenerateAuthTokenString(int id, string username)
        {
            string auth = BCrypt.Net.BCrypt.HashString($"{_random.Next()}{id}{_random.Next()}{username}{_random.Next()}");
            return EncryptionHelper.EncodeBase64String(auth);
        }
        #endregion

        /// <summary>
        /// Hashes a Password
        /// </summary>
        /// <param name="password">The password to hash</param>
        /// <returns>The hashed password</returns>
        public virtual string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password, WorkFactor + _random.Next(0, 3));

        /// <summary>
        /// Verifies if a given Hashed Password is correct
        /// </summary>
        /// <param name="password">The password</param>
        /// <param name="hash">The hashed password</param>
        /// <returns>Whether the Password is valid</returns>
        public virtual bool VerifyPassword(string password, string hash) => BCrypt.Net.BCrypt.Verify(password, hash);
    }
}
