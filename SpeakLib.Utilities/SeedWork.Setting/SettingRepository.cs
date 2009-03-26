using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;

namespace SpeakFriend.Utilities
{
    public class SettingRepository : RepositoryDb, ISettingRepository 
    {
        private readonly ISession _session;

        public SettingRepository(ISession session) : base(session)
        {
            _session = session;
        }

        public void Create(Setting setting)
        {
            _session.Save(setting);
        }

        public void Update(Setting setting)
        {
            _session.Update(setting);
            _session.Flush();
        }

        public void CreateOrUpdate(Setting setting)
        {
            _session.SaveOrUpdate(setting);
        }

        public void Delete(Setting setting)
        {
            _session.Delete(setting);
            _session.Flush();
        }

        public SettingList GetAll()
        {
            return new SettingList(_session.CreateCriteria(typeof (Setting)).List<Setting>());
        }

        public Setting GetById(int settingId)
        {
            return _session.CreateCriteria(typeof(Setting))
                    .Add(Restrictions.Eq("Id", settingId))
                    .UniqueResult<Setting>();
        }

        public SettingList GetBy(SettingSearchDesc searchDesc)
        {
            var criteria = GetExecutable<Setting>();

            AddConditions(searchDesc, criteria);
            AddOrderBy(criteria, searchDesc.OrderBy);

            SetTotalItemCount(criteria, searchDesc);
            SetPager(criteria, searchDesc);

            return new SettingList(criteria.List<Setting>());
        }

        private void AddConditions(SettingSearchDesc searchDesc, ICriteria criteria)
        {
            AddGenericConditions(criteria, searchDesc.Filter);
        }
    }
}
