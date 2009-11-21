using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpeakFriend.Utilities;
using SpeakFriend.Utilities.Usefulness;

namespace Tests.Usefulness
{
	public class UsefulEntity : IPersistable, ICanBeUseful
	{
		public int Id { get; set; }
		public DateTime DateCreated{get; set;}

		public UsefulnessValue Usefulness { get; set; }
	}
}
