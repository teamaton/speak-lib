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
	}
}
