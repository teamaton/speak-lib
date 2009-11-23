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

		private UsefulnessValue _usefulness;
		public UsefulnessValue Usefulness
		{
			get { return _usefulness ?? (_usefulness = _usefulnessValueFactory(this)); }
			set { _usefulness = value; }
		}

		public UsefulEntity(UsefulnessValue.Factory usefulnessValueFactory)
		{
			_usefulnessValueFactory = usefulnessValueFactory;
		}
	}
}
