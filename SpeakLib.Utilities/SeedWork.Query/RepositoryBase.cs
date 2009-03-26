using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace SpeakFriend.Utilities
{
    public abstract class RepositoryDb
    {
        private readonly ISession _session;

        protected RepositoryDb(ISession session)
        {
            _session = session;
        }

        public IList<T> GetBy<T>(ISearchDesc searchDesc)
        {
            var criteria = GetExecutable<T>();

            AddGenericConditions(criteria, searchDesc.Filter);
            AddOrderBy(criteria, searchDesc.OrderBy);

            SetTotalItemCount(criteria, searchDesc);
            SetPager(criteria, searchDesc);

            return criteria.List<T>();
        }

        public DetachedCriteria GetDetachedCriteria<T>()
        {
            return DetachedCriteria.For(typeof (T), typeof (T).Name.ToLower());
        }

        public ICriteria GetExecutable<T>()
        {
            return GetDetachedCriteria<T>().GetExecutableCriteria(_session);
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

    }
}
