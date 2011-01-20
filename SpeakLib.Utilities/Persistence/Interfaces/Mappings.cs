using FluentNHibernate.Mapping;

namespace SpeakFriend.Utilities
{
	public abstract class PersistableBaseMapping<T> : ClassMap<T>
		where T : PersistableBase
	{
		protected PersistableBaseMapping()
		{
			Id(x => x.Id);
			Map(x => x.DateCreated);
		}
	}

	public abstract class MutablePersistableBaseMapping<T> : PersistableBaseMapping<T>
		where T : MutablePersistableBase
	{
		protected MutablePersistableBaseMapping()
		{
			Map(x => x.DateModified);
		}
	}
}