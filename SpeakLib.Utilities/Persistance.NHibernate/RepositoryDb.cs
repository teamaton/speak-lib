using System;
using System.Collections;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;

namespace SpeakFriend.Utilities
{
    public abstract class RepositoryDb<TDomainObject, TDomainObjectList>
        where TDomainObject : IPersistable
        where TDomainObjectList : PersistableList<TDomainObject>, new()
    {
        protected readonly ISession _session;
		private TDomainObjectList _allItemsCached;
		protected event EventHandler<RepositoryDbEventArgs> OnItemMutated;
    	protected event EventHandler<RepositoryDbEventArgs> OnItemCreated;
		protected event EventHandler<RepositoryDbEventArgs> OnItemDeleted;
		protected event EventHandler<RepositoryDbEventArgs> OnItemUpdated;

        /// <summary>
        /// Occurs after a TDomainObject is retrieved from DB
        /// </summary>
        protected event EventHandler<TDomainObjectArgs> AfterItemRetrieved;

        /// <summary>
        /// Occurs after a TDomainObjectList is retrieved from DB
        /// </summary>
        protected event EventHandler<TDomainObjectListArgs> AfterItemListRetrieved;

        protected RepositoryDb(ISession session)
        {
            _session = session;
        	OnItemCreated += ItemMutated;
			OnItemDeleted += ItemMutated;
			OnItemUpdated += ItemMutated;
        }

    	private void ItemMutated(object sender, RepositoryDbEventArgs e)
    	{
			if (OnItemMutated != null)
				OnItemMutated(this, e);
    	}

    	public DetachedCriteria GetDetachedCriteria()
        {
            return DetachedCriteria.For(typeof(TDomainObject), typeof(TDomainObject).Name.ToLower());
        }

        public ICriteria GetExecutableCriteria()
        {
            return GetDetachedCriteria().GetExecutableCriteria(_session);
        }
        
        public void AddGenericConditions(ICriteria criteria, ConditionContainer filter)
        {
            filter.CreateAliases(criteria);
            foreach (Condition condition in filter)
                condition.AddToCriteria(criteria);
        }
        
        public void AddOrderBy(ICriteria criteria, OrderByCriteria orderBy)
        {
            AddOrderBy(criteria, orderBy, null);
        }

		public virtual void AddOrderBy(ICriteria criteria, OrderByCriteria orderByCriteria, string tableAlias)
		{
			if (!orderByCriteria.IsSet()) return;

			foreach (var orderBy in orderByCriteria.CurrentList)
			{
				if (orderBy.HasCriteriaAction)
					orderBy.CriteriaAction(criteria);

				AddOrderBy(criteria, orderBy, orderBy.HasAlias ? orderBy.Alias : tableAlias);
			}
		}

    	private static void AddOrderBy(ICriteria criteria, OrderBy orderBy, string tableAlias)
    	{
			if (orderBy == null) return;

    		var propertyName = (string.IsNullOrEmpty(tableAlias)
    		                    	? string.Empty
    		                    	: tableAlias.EnsureEndsWith("."))
    		                   + orderBy.PropertyName;

    		if (orderBy.Direction == OrderDirection.Ascending)
    			criteria.AddOrder(Order.Asc(propertyName));
    		else
    			criteria.AddOrder(Order.Desc(propertyName));
    	}

    	public void SetPager(ICriteria criteria, IPager pager)
        {
            if (!pager.QueryAll)
            {
                criteria.SetMaxResults(pager.PageSize);
                criteria.SetFirstResult(pager.FirstResult);
            }
        }

        public void SetTotalItemCount(ICriteria criteria, IPager pager)
        {
            var criteriaRowCount = CriteriaTransformer.TransformToRowCount(criteria);
            pager.TotalItems = criteriaRowCount.UniqueResult<int>();
        }

        public virtual void Create(TDomainObject domainObject)
        {
            domainObject.DateCreated = DateTime.Now;
			if (domainObject is IMutablePersistable)
				(domainObject as IMutablePersistable).DateModified = DateTime.Now;

			_session.Save(domainObject);
            ClearAllItemCache();

			if (OnItemCreated != null)
				OnItemCreated(this, new RepositoryDbEventArgs(domainObject));
        }

    	public virtual void Update(TDomainObject domainObject)
        {
			if (domainObject is IMutablePersistable)
				(domainObject as IMutablePersistable).DateModified = DateTime.Now;

            _session.Update(domainObject);
			ClearAllItemCache();

			if (OnItemUpdated != null)
				OnItemUpdated(this, new RepositoryDbEventArgs(domainObject));
		}

        public virtual void CreateOrUpdate(TDomainObject domainObject)
		{
        	var creating = domainObject.Id == 0;

			if (domainObject.DateCreated == DateTime.MinValue)
				domainObject.DateCreated = DateTime.Now;

			if (domainObject is IMutablePersistable)
				(domainObject as IMutablePersistable).DateModified = DateTime.Now;

			_session.SaveOrUpdate(domainObject);
			ClearAllItemCache();

			if (creating && OnItemCreated != null)
				OnItemCreated(this, new RepositoryDbEventArgs(domainObject));
			else if(!creating && OnItemUpdated != null)
				OnItemUpdated(this, new RepositoryDbEventArgs(domainObject));				
		}

        public virtual void Delete(TDomainObject domainObject)
        {
            _session.Delete(domainObject);
            ClearAllItemCache();
            Flush();

			if (OnItemDeleted != null)
				OnItemDeleted(this, new RepositoryDbEventArgs(domainObject));
		}

        public virtual void Delete(int id)
        {
            Delete(GetById(id));
        }

        private void ClearAllItemCache()
        {
            _allItemsCached = null;
        }

        public virtual TDomainObjectList GetAll()
        {
            var list = new TDomainObjectList();

            if (_allItemsCached != null && _allItemsCached.Count != 0)
                list.AddRange(_allItemsCached);
            else
            {
                list.AddRange(_session.CreateCriteria(typeof (TDomainObject))
                                  .List<TDomainObject>());

                _allItemsCached = new TDomainObjectList();
                _allItemsCached.AddRange(list);
            }

            if (AfterItemListRetrieved != null)
                AfterItemListRetrieved(this, new TDomainObjectListArgs(list));

            return list;
        }
        
        public virtual TDomainObject GetById(int id)
        {
            var result = _session.CreateCriteria(typeof(TDomainObject))
                           .Add(Restrictions.Eq("Id", id))
                           .UniqueResult<TDomainObject>();

            if (AfterItemRetrieved != null)
                AfterItemRetrieved(this, new TDomainObjectArgs(result));

            return result;
        }

        public virtual TDomainObjectList GetByIds(params int[] ids)
        {
            var list = new TDomainObjectList();
        	list.AddRange(_session.CreateCriteria(typeof (TDomainObject))
        	              	.Add(Restrictions.In("Id", ids))
        	              	.List<TDomainObject>());

			if (AfterItemListRetrieved != null)
				AfterItemListRetrieved(this, new TDomainObjectListArgs(list));

            return list;
        }

		public virtual TDomainObjectList GetBy(ISearchDesc searchDesc)
		{
			return GetBy(searchDesc, null);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="searchDesc"></param>
		/// <param name="criteriaExtender">Here you can plug in additional changes of the criteria.</param>
		/// <returns></returns>
        public TDomainObjectList GetBy(ISearchDesc searchDesc, Action<ICriteria> criteriaExtender)
        {
            var criteria = GetExecutableCriteria();

            AddGenericConditions(criteria, searchDesc.Filter);
            AddOrderBy(criteria, searchDesc.OrderBy);

			if (criteriaExtender != null)
				criteriaExtender.Invoke(criteria);

            SetTotalItemCount(criteria, searchDesc);
            SetPager(criteria, searchDesc);

            var list = new TDomainObjectList();
            list.AddRange(criteria.List<TDomainObject>());

            if (AfterItemListRetrieved != null)
                AfterItemListRetrieved(this, new TDomainObjectListArgs(list));

            return list;
        }

        public void Flush()
        {
            _session.Flush();
		}

		protected class RepositoryDbEventArgs : EventArgs
		{
			private readonly TDomainObject _item;
			public TDomainObject Item { get { return _item; } }

			public RepositoryDbEventArgs(TDomainObject item)
			{
				_item = item;
			}
		}

        protected class TDomainObjectArgs : RepositoryDbEventArgs
        {
            public TDomainObjectArgs(TDomainObject item) : base(item)
            {
            }
        }

        protected class TDomainObjectListArgs : EventArgs
        {
			private readonly TDomainObjectList _items;
			public TDomainObjectList Items { get { return _items; } }

            public TDomainObjectListArgs(TDomainObjectList items)
			{
				_items = items;
			}            
        }

    	public IList<int> GetAllIds()
    	{
    		DetachedCriteria queryObjects =
    			DetachedCriteria.For(typeof(TDomainObject), "o");

    		ICriteria criteria = queryObjects.GetExecutableCriteria(_session)
    			.SetProjection(Projections.Property("Id")); // guaranteed to exist by IPersistable

    		return criteria.List<int>();
    	}

    	public IList<int> GetAllIds(ISearchDesc searchDesc)
    	{
			var criteria = GetExecutableCriteria();

			AddGenericConditions(criteria, searchDesc.Filter);
			AddOrderBy(criteria, searchDesc.OrderBy);

			criteria.SetProjection(Projections.Property("Id")); // guaranteed to exist by IPersistable

    		return criteria.List<int>();
    	}

		public IList GetProjectionBy(ISearchDesc searchDesc, params string[] projectionProperties)
		{
			var criteria = GetExecutableCriteria();

			AddGenericConditions(criteria, searchDesc.Filter);
			AddOrderBy(criteria, searchDesc.OrderBy);

			foreach (var property in projectionProperties)
				criteria.SetProjection(Projections.Property(property));

			return criteria.List();
		}
    }
}

