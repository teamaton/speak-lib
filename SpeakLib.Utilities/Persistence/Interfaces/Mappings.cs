using FluentNHibernate.Mapping;

namespace SpeakFriend.Utilities
{
	/// <summary>
	/// Defines mapping for <see cref="PersistableBase.Id"/> and <see cref="PersistableBase.DateCreated"/>.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class PersistableBaseMapping<T> : ClassMap<T>
		where T : PersistableBase
	{
		protected PersistableBaseMapping()
		{
			Id(x => x.Id);
			Map(x => x.DateCreated);
		}
	}

	/// <summary>
	/// Defines mapping for <see cref="PersistableBase.Id"/>, <see cref="PersistableBase.DateCreated"/>
	/// and <see cref="MutablePersistableBase.DateModified"/>.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class MutablePersistableBaseMapping<T> : PersistableBaseMapping<T>
		where T : MutablePersistableBase
	{
		protected MutablePersistableBaseMapping()
		{
			Map(x => x.DateModified);
		}
	}
}