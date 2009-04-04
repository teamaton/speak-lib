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

        protected RepositoryDb(){}

        protected RepositoryDb(ISession session)
        {
            _session = session;
        }

        public DetachedCriteria GetDetachedCriteria<T>()
        {
            return DetachedCriteria.For(typeof(T));
        }

        public ICriteria GetExecutable<T>()
        {
            return GetDetachedCriteria<T>().GetExecutableCriteria(_session);
        }

        /*
            public void AddGenericConditions(ICriteria criteria, ConditionContainer filter)
            {
                foreach (Condition condition in filter)
                    condition.AddToCriteria(criteria);
            }

            public void AddOrderBy(ICriteria criteria, OrderByCriteria orderBy)
            {
                AddOrderBy(criteria, orderBy, null);
            }

            public void AddOrderBy(ICriteria criteria, OrderByCriteria orderBy, string tableAlias)
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

            public void SetPager(ICriteria criteria, Pager pager)
            {
                if (!pager.QueryAll)
                {
                    criteria.SetMaxResults(pager.PageSize);
                    criteria.SetFirstResult(pager.FirstResult);
                }
            }

            public void SetTotalItemCount(ICriteria criteria, Pager pager)
            {
                var criteriaRowCount = CriteriaTransformer.TransformToRowCount(criteria);
                pager.TotalItems = criteriaRowCount.UniqueResult<int>();
            }
        */

        public virtual void Create(TDomainObject domainObject)
        {
            domainObject.Created = DateTime.Now;
            _session.Save(domainObject);
        }

        public virtual void Update(TDomainObject domainObject)
        {
            _session.Update(domainObject);
        }

        public virtual void Delete(TDomainObject domainObject)
        {
            _session.Delete(domainObject);
        }

        public virtual void Delete(int id)
        {
            Delete(GetById(id));
        }

        public virtual TDomainObjectList GetAll()
        {
            var list = new TDomainObjectList();
            list.AddRange(_session.CreateCriteria(typeof(TDomainObject))
                                 .List<TDomainObject>());
            return list;
        }

        public virtual TDomainObject GetById(int id)
        {
            return _session.CreateCriteria(typeof(TDomainObject))
                           .Add(Restrictions.Eq("Id", id))
                           .UniqueResult<TDomainObject>();
        }
    }

}

