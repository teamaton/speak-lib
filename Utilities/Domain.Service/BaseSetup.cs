using System.Collections.Generic;
using System.Linq;

namespace SpeakFriend.Utilities
{
    public abstract class BaseSetup<TData, TDerivedClass>
        where TDerivedClass : BaseSetup<TData, TDerivedClass>
    {
        private readonly IDataService<TData> _dataService;

        private readonly List<TData> _itemsToCreate = new List<TData>();
        public List<TData> Created = new List<TData>();

        public TData LastAdded { get { return _itemsToCreate.Last(); } }
        public TData LastCreated { get { return Created.Last(); } }

        protected BaseSetup(IDataService<TData> dataService)
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

        public TDerivedClass Add(TData data)
        {
            _itemsToCreate.Add(data);

            return (TDerivedClass)this;
        }

        public abstract TData Get();

        public TDerivedClass Persist()
        {
            foreach (var customer in _itemsToCreate)
            {
                _dataService.Create(customer);
                Created.Add(customer);
            }

            _itemsToCreate.Clear();    
            return (TDerivedClass)this;
        }

        /// <summary> Persists a setup subject and returns it. </summary>
        public TData GetPersisted()
        {
            var subject = Get();
            Add(subject);
            Persist();
            return subject;
        }

    }
}
