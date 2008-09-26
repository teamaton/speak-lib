using System.Collections.Generic;
using System.Linq;

namespace SpeakFriend.Utilities
{
    public abstract class BaseSetup<TSubject, TDerivedClass> where TDerivedClass : BaseSetup<TSubject, TDerivedClass>
    {
        private readonly IDataService<TSubject> _dataService;

        private readonly List<TSubject> _itemsToCreate = new List<TSubject>();
        public           List<TSubject> Created        = new List<TSubject>();

        public TSubject LastAdded { get { return _itemsToCreate.Last(); } }

        protected BaseSetup(IDataService<TSubject> dataService)
        {
            _dataService = dataService;
        }

        public TDerivedClass Add()
        {
            return Add(Get());
        }

        public TDerivedClass Add(int amount)
        {
            for (int i = 0; i < amount; i++)
                Add(Get());

            return (TDerivedClass)this;
        }

        public TDerivedClass Add(TSubject subject)
        {
            _itemsToCreate.Add(subject);

            return (TDerivedClass)this;
        }

        public abstract TSubject Get();

        public void Persist()
        {
            foreach (var customer in _itemsToCreate)
            {
                _dataService.Create(customer);
                Created.Add(customer);
            }

            _itemsToCreate.Clear();
        }


        /// <summary> Persists a setup subject and returns it. </summary>
        public TSubject GetPersisted()
        {
            var subject = Get();
            Add(subject);
            Persist();
            return subject;
        }

    }
}
