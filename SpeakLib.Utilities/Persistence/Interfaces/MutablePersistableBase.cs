using System;

namespace SpeakFriend.Utilities
{
	public abstract class MutablePersistableBase : PersistableBase, IMutablePersistable
	{
		protected MutablePersistableBase()
		{
			_dateModified = DateTime.Now;
		}

		private DateTime _dateModified;

		public virtual DateTime DateModified
		{
			get { return _dateModified; }
			set { _dateModified = value; }
		}
	}
}