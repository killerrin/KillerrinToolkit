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

        #region AuthToken
        public virtual IAuthToken VerifyAuthToken(string authToken)
        {
            if (string.IsNullOrWhiteSpace(authToken))
                return null;

            var token = _authTokenRepository.GetAllQuery()
                .Where(x => x.Token.Equals(authToken))
                .FirstOrDefault();

            return token;
        }

        public virtual IAuthToken VerifyAuthTokenAndID(int userID, string authToken)
        {
            if (string.IsNullOrWhiteSpace(authToken))
                return null;

            var token = _authTokenRepository.GetAllQuery()
                .Where(x => x.UserId == userID)
                .Where(x => x.Token.Equals(authToken))
                .FirstOrDefault();

            return token;
        }

        public virtual IAuthToken VerifyAuthTokenAndUsername(string username, string authToken)
        {
            if (string.IsNullOrWhiteSpace(username))
                return null;
            if (string.IsNullOrWhiteSpace(authToken))
                return null;

            // Get the User
            var user = _userRepository.GetAllQuery().Where(x => x.Username.ToLower().Equals(username.ToLower())).FirstOrDefault();
            if (user == null)
                return null;

            // Get the Auth Token
            var token = _authTokenRepository.GetAllQuery()
                .Where(x => x.UserId == user.Id)
                .Where(x => x.Token.Equals(authToken))
                .FirstOrDefault();

            return token;
        }

        public virtual string GenerateAuthToken(int id, string username)
        {
            string auth = BCrypt.Net.BCrypt.HashString($"{id}{username}{_random.Next()}");
            return EncryptionHelper.EncodeBase64String(auth);
        }
        #endregion

        #region Password
        protected int WorkFactor { get; set; } = 12;
        public virtual string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password, WorkFactor + _random.Next(0, 3));
        public virtual bool VerifyPassword(string password, string hash) => BCrypt.Net.BCrypt.Verify(password, hash);
        #endregion

        #region Validations
        public string UsernameRegex { get; set; } = @"^[a-zA-Z0-9\s]";
        public virtual bool ValidateUsername(string username)
        {
            if (!IntHelpers.IsBetween(username.Length, 1, 40))
                return false;

            if (!Regex.IsMatch(username, UsernameRegex))
                return false;

            return true;
        }

        public string EmailRegex { get; set; } = @"^[\w!#$%&'*+/=?`{|}~^-]+(?:\.[!#$%&'*+/=?`{|}~^-]+)*@(?:[A-Z0-9-]+\.)+[A-Z]{2,6}$";
        public virtual bool ValidateEmail(string email)
        {
            if (Regex.IsMatch(email, EmailRegex))
                return false;
            return true;
        }

        public PasswordScore MinimumPasswordStrength { get; set; } = PasswordScore.Medium;
        public virtual bool ValidatePassword(string password)
        {
            PasswordScore passwordStrength = CheckPasswordStrength(password);
            Debug.WriteLine($"{passwordStrength.ToString()}");
            if ((int)passwordStrength >= (int)MinimumPasswordStrength)
                return true;
            return false;
        }
        #endregion

        public virtual void FakeHash() { BCrypt.Net.BCrypt.HashPassword("", WorkFactor); }
        public virtual PasswordScore CheckPasswordStrength(string password)
        {
            int score = 0;

            if (string.IsNullOrWhiteSpace(password) || password.Length < 1)
                return PasswordScore.Blank;

            if (password.Length < 4)
                return PasswordScore.VeryWeak;

            if (password.Length >= 8)
                score++;
            if (password.Length >= 10)
                score++;

            if (Regex.Match(password, @"\d", RegexOptions.ECMAScript).Success)
            {
                score++;
            }

            if (Regex.Match(password, @"[a-z]", RegexOptions.ECMAScript).Success && Regex.Match(password, @"[A-Z]", RegexOptions.ECMAScript).Success)
            {
                score++;
            }

            if (Regex.Match(password, @"[!,@,#,$,%,^,&,*,?,_,~,-,£,(,)]", RegexOptions.ECMAScript).Success)
            {
                score++;
            }

            Debug.WriteLine($"Password: {password}, Length: {password.Length}, Score: {score}");
            return (PasswordScore)score;
        }
        public virtual bool IsExpired(DateTime? utcExpiry)
        {
            if (utcExpiry == null) return false;
            return utcExpiry >= DateTime.UtcNow;
        }

    }
}
