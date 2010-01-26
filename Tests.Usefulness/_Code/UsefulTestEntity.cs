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

		#region Usefulness

		// ReSharper disable UnusedAutoPropertyAccessor.Local
		private int UsefulPositive { get; set; }
		private int UsefulNegative { get; set; }
		// ReSharper restore UnusedAutoPropertyAccessor.Local

		private UsefulnessValue _usefulness;

		public UsefulnessValue Usefulness
		{
			get { return _usefulness ?? (_usefulness = new UsefulnessValue(UsefulPositive, UsefulNegative)); }
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
