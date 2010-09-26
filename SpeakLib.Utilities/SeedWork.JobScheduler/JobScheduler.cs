using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Quartz;
using Quartz.Impl;

namespace SpeakFriend.Utilities
{
	/// <summary>
	/// 
	/// </summary>
	/// <remarks>http://stackoverflow.com/questions/1356789/quartz-net-with-asp-net</remarks>
	public class JobScheduler
	{
		private readonly NameValueCollection _properties;

		private readonly ISchedulerFactory _schedulerFactory;

		public string GroupName { get; private set; }

		public IScheduler Scheduler { get; private set; }

		/// <summary>
		/// Creates a new JobScheduler which provides an in-process scheduler.
		/// </summary>
		/// <param name="groupName">Name for a group of jobs and triggers.</param>
		public JobScheduler(string groupName)
		{
			GroupName = groupName;
			_properties = new NameValueCollection();
			_properties["quartz.scheduler.instanceName"] = "RemoteServer";

			// set thread pool info
			_properties["quartz.threadPool.type"] = "Quartz.Simpl.SimpleThreadPool, Quartz";
			_properties["quartz.threadPool.threadCount"] = "5";
			_properties["quartz.threadPool.threadPriority"] = "Normal";

			_schedulerFactory = new StdSchedulerFactory(_properties);
		}

		public JobScheduler StartNew()
		{
			Scheduler = _schedulerFactory.GetScheduler();
			Scheduler.Start();
			return this;
		}

		public IOurJob CreateJob(string jobName, Action action)
		{
			throw new NotImplementedException(
				"This method has to be reviewed. Seems like we can't use on class (JobBase) for all Jobs.");
			return new JobBase(this, jobName, action);
		}
	}
}
