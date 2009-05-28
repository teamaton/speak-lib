using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;

namespace SpeakFriend.Utilities
{
    public class SettingRepository : RepositoryDb<Setting, SettingList>, ISettingRepository 
    {
        public SettingRepository(ISession session) : base(session)
        {
        }

        public override void Update(Setting setting)
        {
            base.Update(setting);
            Flush();
        }

        public override void Delete(Setting setting)
        {
            base.Delete(setting);
            Flush();
        }

        public SettingList GetBy(SettingSearchDesc searchDesc)
        {
            return GetBy(searchDesc as ISearchDesc);
        }

        public Setting GetUnique(SettingSearchDesc searchDesc)
        {
            ICriteria criteria = GetCriteria(searchDesc);

            return criteria.UniqueResult<Setting>();
        }

        private ICriteria GetCriteria(SettingSearchDesc searchDesc)
        {
            var criteria = GetExecutableCriteria();

            AddConditions(searchDesc, criteria);
            AddOrderBy(criteria, searchDesc.OrderBy);

            SetTotalItemCount(criteria, searchDesc);
            SetPager(criteria, searchDesc);
            return criteria;
        }

        private void AddConditions(SettingSearchDesc searchDesc, ICriteria criteria)
        {
            AddGenericConditions(criteria, searchDesc.Filter);
        }
    }
}
