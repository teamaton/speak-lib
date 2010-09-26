using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using NUnit.Framework;
using SpeakFriend.Utilities;

namespace Tests.Utilities
{
	[TestFixture]
	public class JobSchedulerTest
	{
		[Test]
		[Ignore("Implementation not finished yet")]
		public void DefineJobAndExecute()
		{
			Console.WriteLine("Defining job and trigger");

			new JobScheduler("test").StartNew()
				.CreateJob("Testjob", ()=>
				                      	{
											using (var fs = File.Open("scheduler-out.txt", FileMode.Append, FileAccess.Write))
											{
												var bytes = new UTF8Encoding().GetBytes("writing");
												fs.Write(bytes, 0, bytes.Length);
												fs.Close();
											}
				                      	})
				.ScheduleAt(DateTime.UtcNow.AddSeconds(1), new TimeSpan(0,0,1));

			Console.WriteLine("Defined job and trigger - waiting");

			for (int i = 0; i < 3; i++)
			{
				Thread.Sleep(1000);
				Console.Write(".");
			}

			Assert.That(File.Exists("scheduler-out.txt"));
		}
	}
}
