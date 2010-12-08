using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities
{
    [Serializable]
    public abstract class PersistableList<T> : List<T> where T : IPersistable
    {
        private ISet<T> _persistentItems = new HashSet<T>();
// ReSharper disable UnusedMember.Local
        private ISet<T> Items
// ReSharper restore UnusedMember.Local
        {
            get
            {
                _persistentItems.Clear();
                _persistentItems.UnionWith(this);
                return _persistentItems;
            }
            set
            {
                _persistentItems = value;
                Clear();
                AddRange(value);
            }
        }

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
