using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities.Usefulness
{
	public class UsefulnessCreatorAnonymous : IUsefulnessCreator
	{
		public int Id { get; set; }
		public string Type { get { return GetType().Name; } }
		public string IpAddress { get; set; }
		/// <summary>
		/// The time span into the past during which an anonymous visitor can not submit 
		/// new usefulness ratings from the same IP address.
		/// </summary>
		public TimeSpan? BlockingPeriod { get; set; }
	}
}
