using System;
using System.Security.Cryptography;
using System.Text;

namespace SpeakFriend.Utilities
{
	/// <summary>
	/// A class to encapsulate a Password and its own unique salt value.
	/// Stores only the salt and the salted hash value - the plain text password is never saved.
	/// </summary>
	/// <remarks>
	/// via: http://www.aspheute.com/artikel/20040105.htm
	/// </remarks>
	[Serializable]
	public class Password
	{
		public virtual int Salt { get; private set; }

		public virtual string SaltedPasswordHash { get; private set; }

		protected Password()
		{
			
		}

		public Password(string strPassword)
		{
			Salt = CreateRandomSalt();
			SaltedPasswordHash = ComputeSaltedHash(strPassword);
		}

		public static string CreateRandomPassword(int passwordLength)
		{
			var allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ23456789";
			var randomBytes = new Byte[passwordLength];
			var rng = new RNGCryptoServiceProvider();
			rng.GetBytes(randomBytes);
			var chars = new char[passwordLength];
			var allowedCharCount = allowedChars.Length;

			for (var i = 0; i < passwordLength; i++)
			{
				chars[i] = allowedChars[randomBytes[i] % allowedCharCount];
			}

			return new string(chars);
		}

		public static int CreateRandomSalt()
		{
			var saltBytes = new Byte[4];
			var rng = new RNGCryptoServiceProvider();
			rng.GetBytes(saltBytes);

			return (((saltBytes[0]) << 24) + ((saltBytes[1]) << 16) +
					((saltBytes[2]) << 8) + (saltBytes[3]));
		}

		public virtual string ComputeSaltedHash(string strPassword)
		{
			// Create Byte array of password string
			var encoder = new ASCIIEncoding();
			var secretBytes = encoder.GetBytes(strPassword);

			// Create a new salt
			var saltBytes = new Byte[4];
			saltBytes[0] = (byte)(Salt >> 24);
			saltBytes[1] = (byte)(Salt >> 16);
			saltBytes[2] = (byte)(Salt >> 8);
			saltBytes[3] = (byte)(Salt);

			// append the two arrays
			var toHash = new Byte[secretBytes.Length + saltBytes.Length];
			Array.Copy(secretBytes, 0, toHash, 0, secretBytes.Length);
			Array.Copy(saltBytes, 0, toHash, secretBytes.Length, saltBytes.Length);

			var sha1 = SHA1.Create();
			var computedHash = sha1.ComputeHash(toHash);

			return encoder.GetString(computedHash);
		}

		/// <summary>
		/// Checks the given plain text password against the saved salt and salted hash.
		/// </summary>
		public bool Matches(string plainTextPassword)
		{
			return SaltedPasswordHash == ComputeSaltedHash(plainTextPassword);
		}
	}
}