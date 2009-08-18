using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;

namespace SpeakFriend.Utilities
{
    public class SessionFactoryContainer
    {
        private readonly Func<ISessionFactory> _buildSessionFactory;

        public SessionFactoryContainer()
        {
            _buildSessionFactory = ()=> new Configuration().Configure().BuildSessionFactory();
        }

        public SessionFactoryContainer(Func<ISessionFactory> func)
        {
            _buildSessionFactory = func;
        }

        private ISessionFactory _sessionFactory;
    	private static readonly object _sessionFactoryLock = new object();

        public ISessionFactory GetSessionFactory()
        {
			if (_sessionFactory == null)
			{
				lock (_sessionFactoryLock)
				{
					if (_sessionFactory == null)
						_sessionFactory = _buildSessionFactory();
				}
			}
        	return _sessionFactory;
        }
    }
}
