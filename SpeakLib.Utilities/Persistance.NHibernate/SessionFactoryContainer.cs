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
        private ISessionFactory _sessionFactory;
        public ISessionFactory GetSessionFactory()
        {
            if (_sessionFactory == null)
                _sessionFactory = new Configuration().Configure().BuildSessionFactory();

            return _sessionFactory;
        }
    }
}
