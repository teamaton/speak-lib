using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace SpeakFriend.Utilities.Domain.Service
{
    /// <summary>
    /// ! DONT USE, work in progress..
    /// </summary>
    public class RepositoryDbNoIoc<TDomainObject, TDomainObjectList> : 
        RepositoryDb<TDomainObject, TDomainObjectList>
        where TDomainObject : IPersistable
        where TDomainObjectList : PersistableList<TDomainObject>, new()
    {
        public RepositoryDbNoIoc()
        {
           //this(new Is) 
        }

        protected RepositoryDbNoIoc(ISession session)
            : base(session)
        {
        }
    }
}
