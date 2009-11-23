using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpeakFriend.Utilities;
using SpeakFriend.Utilities.Usefulness;

namespace Tests.Usefulness
{
	public class UsefulEntity : IPersistable, IUsefulnessEntity
	{
		private readonly UsefulnessValue.Factory _usefulnessValueFactory;

		public int Id { get; set; }
		public DateTime DateCreated{get; set;}

		public UsefulnessValue Usefulness { get; set; }

		public UsefulEntity(UsefulnessValue.Factory usefulnessValueFactory)
		{
			_usefulnessValueFactory = usefulnessValueFactory;
			Usefulness = _usefulnessValueFactory.Invoke(this);
		}
	}
}
