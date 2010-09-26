using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Quartz;

namespace SpeakFriend.Utilities
{
	public class JobBase : IJob, IOurJob
	{
		private readonly JobScheduler _jobScheduler;
		private readonly string _name;
		private readonly Action _action;

		public JobBase(JobScheduler jobScheduler, string name, Action action)
		{
			_jobScheduler = jobScheduler;
			_name = name;
			_action = action;
		}

		#region Implementation of IJob

		public void Execute(JobExecutionContext context)
		{
			Debugger.Break();
			var action = context.MergedJobDataMap["action"] as Action;
			if (action != null)
				action();
		}

		#endregion

		public JobScheduler ScheduleAt(DateTime startDateTime, TimeSpan timeSpan)
		{
			// define the job and ask it to run
			var job = new JobDetail(_name, _jobScheduler.GroupName, typeof (JobBase))
			          	{
			          		JobDataMap = new JobDataMap(
			          			new Dictionary<string, object> {{"action", _action}})
			          	};

			var trigger = new SimpleTrigger("TriggerFor_" + _name, _jobScheduler.GroupName,
			                                _name, _jobScheduler.GroupName,
			                                DateTime.UtcNow, null,
			                                SimpleTrigger.RepeatIndefinitely, timeSpan);

			// schedule the job
			_jobScheduler.Scheduler.ScheduleJob(job, trigger);
			return _jobScheduler;
		}
	}

	public interface IOurJob
	{
		JobScheduler ScheduleAt(DateTime startDateTime, TimeSpan timeSpan);
	}
}
