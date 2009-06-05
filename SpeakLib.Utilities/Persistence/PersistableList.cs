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
            var result = new List<int>();

            ForEach(item 
                    => result.Add(item.Id));

            return result;
        }
    }
}
