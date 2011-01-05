using System;
using NUnit.Framework;
using SharpTestsEx;
using SpeakFriend.Utilities;

namespace Tests.Utilities.Web
{
	[TestFixture]
	public class PasswordTest
	{
		[Test]
		public void Usage()
		{
			var passwordPlainText = "Passwort";

			// Create
			var pw = new Password(passwordPlainText);

			// Save
			Console.WriteLine("Salt:        {0}", pw.Salt);
			Console.WriteLine("Salted hash: {0}", pw.SaltedPasswordHash);

			pw.Matches(passwordPlainText).Should().Be.True();
		}
	}
}