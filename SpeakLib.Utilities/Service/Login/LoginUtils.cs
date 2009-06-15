using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;
using NHibernate;
using NHibernate.Criterion;

namespace SpeakFriend.Utilities
{
    public static class LoginUtils
    {
        /// <summary>
        /// Returns true, if the given string string has the same hash, 
        /// as the stored database value.
        /// </summary>
        /// <param name="userInput"></param>
        /// <param name="hashedMd5"></param>
        /// <returns></returns>
        public static bool IsValidPassword(string userInput, string hashedMd5)
        {
            return GetPasswordHash(userInput) == hashedMd5;
        }

        /// <summary>
        /// Converts the given Password to an md5 Hash
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string GetPasswordHash(string password)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(password.Trim(), "MD5");
        }

        /// <summary>
        /// Returns the next available username based on the last entries in the DB.
        /// </summary>
        /// <returns></returns>
        public static string GetNextUsername(ISession session, Type userType, string nameColumn)
        {
            var lastUsername = session.CreateCriteria(userType).SetProjection(Projections.Property(nameColumn)).AddOrder(
                Order.Desc(nameColumn)).SetMaxResults(1).UniqueResult<string>();

            var idx = Convert.ToInt32(lastUsername.Substring(1));
            idx++;

            var newUsername = lastUsername.Substring(0, 1) + idx.ToString("000000"); // always use 6 digits (I apologize to the millionth user ;-))
            return newUsername;
        }

        public static string GetRandomPassword()
        {
            var password = CreateRandomPassword(8);
            return password;
        }

        public static string CreateRandomPassword(int PasswordLength)
        {
            const string _allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ23456789";
            var randomBytes = new Byte[PasswordLength];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomBytes);
            var chars = new char[PasswordLength];
            int allowedCharCount = _allowedChars.Length;

            for (int i = 0; i < PasswordLength; i++)
            {
                chars[i] = _allowedChars[(int)randomBytes[i] % allowedCharCount];
            }

            return new string(chars);
        }

        private const string numbers = "0123456789";
        private const string lowerCase = "abcdefghijkmnopqrstuvwxyz";
        private const string upperCase = "ABCDEFGHJKLMNOPQRSTUVWXYZ";

        public static char[][] MandatoryChars = new[] { numbers.ToCharArray(), lowerCase.ToCharArray(),
                                                            upperCase.ToCharArray() };

        public const int PasswordMinLength = 8;

        public static bool IsPasswordOk(string password)
        {
            if (string.IsNullOrEmpty(password))
                return false;

            if (password.Length < PasswordMinLength)
                return false;

            for (int i = 0; i < MandatoryChars.Length; i++)
                if (password.IndexOfAny(MandatoryChars[i]) < 0)
                    return false;

            return true;
        }
    }
}
