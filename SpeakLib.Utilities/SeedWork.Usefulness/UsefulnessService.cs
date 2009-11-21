using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace SpeakFriend.Utilities.Usefulness
{
	public class UsefulnessService : RepositoryDb<UsefulnessEntry, UsefulnessEntryList>
	{
		public UsefulnessService(ISession session) : base(session)
		{
		}
	}
}
