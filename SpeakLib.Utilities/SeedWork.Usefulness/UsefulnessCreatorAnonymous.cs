using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities.Usefulness
{
	[Serializable]
	public class UsefulnessCreatorAnonymous : IUsefulnessCreator
	{
		public UsefulnessCreatorAnonymous(string ipAddress, TimeSpan blockingTimeSpan)
		{
			IpAddress = ipAddress;
			BlockingPeriod = blockingTimeSpan;
		}

		public int Id { get; set; }
		public string TypeName { get { return GetType().Name; } }

		#region Not persisted

		public string IpAddress { get; set; }
		/// <summary>
		/// The time span into the past during which an anonymous visitor can not submit 
		/// new usefulness ratings from the same IP address.
		/// </summary>
		public TimeSpan? BlockingPeriod { get; set; }

		#endregion
	}
}
