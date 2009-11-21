using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using SpeakFriend.Utilities;
using Tests.Usefulness;

namespace Tests.Usefulness
{
	public class UsefulEntityList : PersistableList<UsefulEntity>
	{
		
	}

	public class UsefulEntityService : RepositoryDb<UsefulEntity, UsefulEntityList>
    {
        public UsefulEntityService(ISession session) : base(session)
        {
        }
    }
}
