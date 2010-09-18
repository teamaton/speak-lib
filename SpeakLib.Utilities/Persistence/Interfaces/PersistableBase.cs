using System;

namespace SpeakFriend.Utilities
{
	/// <summary>
	/// Implements <see cref="IPersistable"/> with <see cref="Id"/> and <see cref="DateCreated"/>, 
	/// and initializes <see cref="DateCreated"/> with the current <see cref="DateTime"/> in the constructor.
	/// </summary>
	public abstract class PersistableBase : IPersistable
	{
		protected PersistableBase()
		{
			_dateCreated = DateTime.Now;
		}

		#region Implementation of IPersistable

		public virtual int Id { get; set; }
		private DateTime _dateCreated;

		public virtual DateTime DateCreated
		{
			get { return _dateCreated; }
			set { _dateCreated = value; }
		}

		#endregion
	}
}