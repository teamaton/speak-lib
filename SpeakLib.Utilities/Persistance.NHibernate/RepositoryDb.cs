using System;
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

        protected RepositoryDb(ISession session)
        {
            _session = session;
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
                criteria.AddOrder(NHibernate.Criterion.Order.Asc(propertyName));
            else
                criteria.AddOrder(NHibernate.Criterion.Order.Desc(propertyName));
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
            domainObject.Created = DateTime.Now;
            _session.Save(domainObject);
            ClearAllItemCache();
        }

        public virtual void Update(TDomainObject domainObject)
        {
			if (domainObject is IMutablePersistable)
				(domainObject as IMutablePersistable).Modified = DateTime.Now;

            _session.Update(domainObject);
			ClearAllItemCache();
		}

        public void CreateOrUpdate(TDomainObject domainObject)
        {
			if (domainObject is IMutablePersistable)
				(domainObject as IMutablePersistable).Modified = DateTime.Now;

			_session.SaveOrUpdate(domainObject);
			ClearAllItemCache();
		}

        public virtual void Delete(TDomainObject domainObject)
        {
            _session.Delete(domainObject);
            ClearAllItemCache();
            Flush();
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
    }

}

