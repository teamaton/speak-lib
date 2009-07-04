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
            return DetachedCriteria.For(typeof(TDomainObject));
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

        public virtual void AddOrderBy(ICriteria criteria, OrderByCriteria orderBy, string tableAlias)
        {
            var propertyName = "";

            if (!String.IsNullOrEmpty(tableAlias) && !tableAlias.EndsWith("."))
                tableAlias = tableAlias + ".";

            if (orderBy.IsSet())
                propertyName = tableAlias + orderBy.Current.PropertyName;
            else
                return;
            
            if (orderBy.Current.Direction == OrderDirection.Ascending)
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

            if (_allItemsCached != null)
                list.AddRange(_allItemsCached);
            else
            {
                list.AddRange(_session.CreateCriteria(typeof (TDomainObject))
                                  .List<TDomainObject>());

                _allItemsCached = new TDomainObjectList();
                _allItemsCached.AddRange(list);
            }

            return list;
        }
        
        public virtual TDomainObject GetById(int id)
        {
            return _session.CreateCriteria(typeof(TDomainObject))
                           .Add(Restrictions.Eq("Id", id))
                           .UniqueResult<TDomainObject>();
        }

        public TDomainObjectList GetBy(ISearchDesc searchDesc)
        {
            var criteria = GetExecutableCriteria();

            AddGenericConditions(criteria, searchDesc.Filter);
            AddOrderBy(criteria, searchDesc.OrderBy);

            SetTotalItemCount(criteria, searchDesc);
            SetPager(criteria, searchDesc);

            var list = new TDomainObjectList();
            list.AddRange(criteria.List<TDomainObject>());
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

    	public IList<int> GetAllIds()
    	{
    		DetachedCriteria queryObjects =
    			DetachedCriteria.For(typeof(TDomainObject), "o");

    		ICriteria criteria = queryObjects.GetExecutableCriteria(_session)
    			.SetProjection(Projections.Property("Id")); // guaranteed to exist by IPersistable

    		return criteria.List<int>();
    	}
    }
}

