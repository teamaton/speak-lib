using System.Collections.Generic;
using System.Linq;

namespace SpeakFriend.Utilities
{
    /// <summary>
    /// The BaseSetup class provides an easy way to 
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    /// <typeparam name="TSetup"></typeparam>
    public abstract class BaseSetup<TData, TSetup>
        where TSetup : BaseSetup<TData, TSetup>, 
        IHideObjectMembers
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

        public TSetup Add()
        {
            return Add(Get());
        }

        public TSetup Add(int amount)
        {
            for (int i = 0; i < amount; i++)
                Add(Get());

            return (TSetup)this;
        }

        public TSetup Add(TData data)
        {
            _itemsToCreate.Add(data);

            return (TSetup)this;
        }

        public abstract TData Get();

        public TSetup Persist()
        {
            foreach (var item in _itemsToCreate)
            {
                _dataService.Create(item);
                Created.Add(item);
            }

            _itemsToCreate.Clear();    
            return (TSetup)this;
        }

        /// <summary> Persists a setup subject and returns it. </summary>
        public TData GetPersisted()
        {
            var data = Get();
            Add(data);
            Persist();
            return data;
        }

    }
}
