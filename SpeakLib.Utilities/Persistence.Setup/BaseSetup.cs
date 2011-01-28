using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using SpeakFriend.Utilities;

namespace SpeakFriend.Utilities
{
	public abstract class BaseSetup<TSubject, TDerivedClass> where TDerivedClass : BaseSetup<TSubject, TDerivedClass>
	{
		private readonly ISession _session;
		private readonly IDataService<TSubject> _dataService;

		private readonly List<TSubject> _itemsToCreate = new List<TSubject>();

		protected BaseSetup(IDataService<TSubject> dataService)
		{
			_dataService = dataService;
		}

		protected BaseSetup(ISession session)
		{
			_session = session;
		}

		public TSubject LastAdded
		{
			get { return _itemsToCreate.Last(); }
		}

		public List<TSubject> Added
		{
			get { return _itemsToCreate; }
		}

		public List<TSubject> Created = new List<TSubject>();

		public TSubject LastCreated
		{
			get { return Created.Last(); }
		}

		public virtual TDerivedClass Add()
		{
			return Add(Get());
		}

		public virtual TDerivedClass Add(int amount)
		{
			for (var i = 0; i < amount; i++)
				Add(Get());

			return (TDerivedClass) this;
		}

		public virtual TDerivedClass Add(TSubject subject)
		{
			_itemsToCreate.Add(subject);

			return (TDerivedClass) this;
		}

		public abstract TSubject Get();

		public virtual List<TSubject> Get(int amount)
		{
			var result = new List<TSubject>();
			for (var i = 0; i < amount; i++)
			{
				var item = Get();
				result.Add(item);
				Add(item);
			}

			return result;
		}

		public virtual TDerivedClass Persist()
		{
			foreach (var subject in _itemsToCreate)
			{
				if (_dataService != null)
					_dataService.Create(subject);
				else
					_session.Save(subject);
				Created.Add(subject);
			}

			_itemsToCreate.Clear();

			return (TDerivedClass) this;
		}

		public virtual TSubject GetPersisted()
		{
			Add().Persist();
			return Created.Last();
		}

		public virtual TSubject GetPersisted(Func<TSubject, TSubject> modifier)
		{
			Add();
			var subject = modifier(LastAdded);
			Persist();
			return subject;
		}

		public virtual TSubject GetPersisted(Action<TSubject> modifier)
		{
			Add();
			var subject = LastAdded;
			modifier(subject);
			Persist();
			return subject;
		}

		public virtual List<TSubject> GetPersisted(int amount)
		{
			var subjects = Get(amount);
			Persist();
			return subjects;
		}
	}
}