using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities
{
    public abstract class PersistableList<T> : List<T> where T : IPersistable
    {
        protected PersistableList(){}

        protected PersistableList(IEnumerable<T> list)
        {
            AddRange(list);
        }

        public List<int> Ids()
        {
            return (from item in this select item.Id).ToList();
        }
    }
}
