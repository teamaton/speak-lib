using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using SpeakFriend.Utilities;
using SpeakFriend.Utilities.Usefulness;

namespace Tests.Usefulness
{
	public class UsefulTestEntity : IPersistable, IUsefulnessEntity
	{
		public int Id { get; set; }
		public string Type { get { return GetType().Name; } }
		public DateTime DateCreated { get; set; }

		public UsefulTestEntity(UsefulnessValue.Factory usefulnessValueFactory)
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

	public class UsefulTestEntityList : PersistableList<UsefulTestEntity>
	{

	}

	public class UsefulTestEntityService : RepositoryDb<UsefulTestEntity, UsefulTestEntityList>
	{
		public UsefulTestEntityService(ISession session)
			: base(session)
		{
		}
	}
}
