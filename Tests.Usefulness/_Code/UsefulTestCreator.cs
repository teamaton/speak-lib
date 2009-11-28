using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using SpeakFriend.Utilities;
using SpeakFriend.Utilities.Usefulness;

namespace Tests.Usefulness
{
	public class UsefulTestCreator : IPersistable, IUsefulnessCreator
	{
		public int Id { get; set; }
		public string Type { get { return GetType().Name; } }
		public DateTime DateCreated { get; set; }

		#region Usefulness

		#endregion
	}

	public class UsefulTestCreatorList : PersistableList<UsefulTestCreator>
	{

	}

	public class UsefulTestCreatorService : RepositoryDb<UsefulTestCreator, UsefulTestCreatorList>
	{
		public UsefulTestCreatorService(ISession session)
			: base(session)
		{
		}
	}
}
