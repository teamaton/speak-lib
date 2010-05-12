using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Iesi.Collections.Generic;
using NHibernate.Collection;
using NHibernate.Collection.Generic;
using NHibernate.Engine;
using NHibernate.Persister.Collection;
using NHibernate.UserTypes;

namespace SpeakFriend.Utilities
{
	[Serializable]
	public abstract class PersistableSet<T> : List<T>, Iesi.Collections.Generic.ISet<T>, IUserCollectionType where T : IPersistable
	{
		protected PersistableSet() { }

		protected PersistableSet(IEnumerable<T> list)
		{
			AddAll(list.ToList());
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
			foreach (T item in this)
			{
				item.Id = -1;
			}
		}

		#region Implementation of ICloneable

		public object Clone()
		{
			throw new System.NotImplementedException();
		}

		#endregion

		#region Implementation of ISet<T>

		public ISet<T> Union(ISet<T> a)
		{
			throw new System.NotImplementedException();
		}

		public ISet<T> Intersect(ISet<T> a)
		{
			throw new System.NotImplementedException();
		}

		public ISet<T> Minus(ISet<T> a)
		{
			throw new System.NotImplementedException();
		}

		public ISet<T> ExclusiveOr(ISet<T> a)
		{
			throw new System.NotImplementedException();
		}

		public bool ContainsAll(ICollection<T> c)
		{
			throw new System.NotImplementedException();
		}

		public bool Add(T o)
		{
			throw new System.NotImplementedException();
		}

		public bool AddAll(ICollection<T> c)
		{
			throw new System.NotImplementedException();
		}

		public bool RemoveAll(ICollection<T> c)
		{
			throw new System.NotImplementedException();
		}

		public bool RetainAll(ICollection<T> c)
		{
			throw new System.NotImplementedException();
		}

		public bool IsEmpty
		{
			get { throw new System.NotImplementedException(); }
		}

		#endregion

		#region Implementation of IUserCollectionType

		public IPersistentCollection Instantiate(ISessionImplementor session, ICollectionPersister persister)
		{
			throw new System.NotImplementedException();
		}

		public IPersistentCollection Wrap(ISessionImplementor session, object collection)
		{
			throw new System.NotImplementedException();
		}

		public IEnumerable GetElements(object collection)
		{
			throw new System.NotImplementedException();
		}

		public bool Contains(object collection, object entity)
		{
			throw new System.NotImplementedException();
		}

		public object IndexOf(object collection, object entity)
		{
			throw new System.NotImplementedException();
		}

		public object ReplaceElements(object original, object target, ICollectionPersister persister, object owner, IDictionary copyCache, ISessionImplementor session)
		{
			throw new System.NotImplementedException();
		}

		public object Instantiate(int anticipatedSize)
		{
			throw new System.NotImplementedException();
		}

		#endregion
	}
}
