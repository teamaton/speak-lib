using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using NUnit.Framework;
using SpeakFriend.Utilities;

namespace Tests.Service
{
    [TestFixture]
    public class StringExtensionTests
    {
        [Test]
        public void ConvertToInt()
        {
            "22".ToInt32().Should().Be.EqualTo(22);

            (new Action(() => "abc".ToInt32()))
                .Should().Throw<FormatException>();

        }

		[Test]
		public void EnsureFilePathsTest()
		{
			var newDir = Path.Combine(Environment.CurrentDirectory, "test/temp");
			var newFile = Path.Combine(newDir, "file.txt");
			try
			{
				Assert.That(Directory.Exists(newDir), Is.False);
				Assert.Throws<DirectoryNotFoundException>(() => File.Create(newFile), "Should fail bc dir does not exist!");
				newFile.EnsureDirectoryExists();
				var fileStream = File.Create(newFile);
				fileStream.Close();
				Assert.That(File.Exists(newFile));
			}
			catch (IOException ioe)
			{
				Assert.Fail(ioe.Message);
			}
			catch (Exception e)
			{
				Assert.Fail(e.Message);
			}
			finally
			{
				File.Delete(newFile);
				Directory.Delete(newDir);
			}
		}

		[Test]
		public void EnsureDirPathsTest()
		{
			var newDir = Path.Combine(Environment.CurrentDirectory, "test/temp/");
			try
			{
				Assert.That(Directory.Exists(newDir), Is.False);
				newDir.EnsureDirectoryExists();
				Assert.That(Directory.Exists(newDir));
			}
			catch (IOException ioe)
			{
				Assert.Fail(ioe.Message);
			}
			catch (Exception e)
			{
				Assert.Fail(e.Message);
			}
			finally
			{
				Directory.Delete(newDir);
			}
		}

		[Test]
		public void JoinObjectCollection()
		{
			var list = new List<object>
			           	{
			           		4,
			           		"my string",
			           		3.5
			           	};
			Console.WriteLine(list.JoinToString());
		}

		[Test]
		public void JoinEmptyObjectCollection()
		{
			var list = new List<object>();
			list.JoinToString().Should().Be.EqualTo("empty");

			list = null;
			list.JoinToString().Should().Be.EqualTo("null");
		}
    }
}
