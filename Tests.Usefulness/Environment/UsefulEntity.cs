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
		public int Id { get; set; }
		public string Type { get { return GetType().Name; } }
		public DateTime DateCreated { get; set; }

		public UsefulEntity(UsefulnessValue.Factory usefulnessValueFactory)
		{
			_usefulnessValueFactory = usefulnessValueFactory;
		}

		#region Usefulness

		private readonly UsefulnessValue.Factory _usefulnessValueFactory;

		private UsefulnessValue _usefulness;

		public UsefulnessValue Usefulness
		{
			get { return _usefulness ?? (_usefulness = _usefulnessValueFactory(this)); }
			set { _usefulness = value; }
		}

		#endregion
	}
}
