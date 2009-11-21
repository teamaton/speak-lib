using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using SpeakFriend.Utilities;
using SpeakFriend.Utilities.Usefulness;

namespace Tests.Usefulness
{
    class ServiceLocator
    {
    	private static ISession GetOpenSession()
        {
            return new Configuration().Configure()
                .BuildSessionFactory().OpenSession();
        }

		public static UsefulnessService UsefulnessService()
    	{
    		return new UsefulnessService(GetOpenSession());
    	}

    	public static NHibernateHelperSF NHibernateHelper()
    	{
    		return new NHibernateHelperSF(GetOpenSession());
    	}

    	public static UsefulEntityService UsefulEntityService()
    	{
    		return new UsefulEntityService(GetOpenSession());
    	}
    }
}
