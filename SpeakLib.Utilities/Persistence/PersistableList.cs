using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities
{
    [Serializable]
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

        /// <summary>
        /// Sets the IDs to -1
        /// </summary>
        protected void UnsetIds()
        {
            ForEach(T => T.Id = -1);
        }
    }
}
