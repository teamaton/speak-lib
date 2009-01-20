using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using SpeakTag.Tests.Environment;

namespace SpeakTag.Tests._Code
{
    class ServiceLocator
    {
        private static ISession GetOpenSession()
        {
            return new Configuration().Configure()
                .BuildSessionFactory().OpenSession();
        }

        public static TestTargetService TestTargetService(){
            return new TestTargetService(GetOpenSession());
        }

        public static TagPrototypeService TagPrototypeService(){
            return new TagPrototypeService(GetOpenSession());        
        }

        public static TagService TagService(){
            return new TagService(GetOpenSession());
        }
    }
}
