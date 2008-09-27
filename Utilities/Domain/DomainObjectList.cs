using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities
{
    public abstract class DomainObjectList<T> : List<T> where T : IDomainObject
    {
        protected DomainObjectList(){}

        protected DomainObjectList(IEnumerable<T> list)
        {
            AddRange(list);
        }
    }
}
