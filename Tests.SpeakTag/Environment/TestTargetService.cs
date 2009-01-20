using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace SpeakTag.Tests.Environment
{
    public class TestTargetService 
    {
        public ISession _session;

        public TestTargetService(ISession session)
        {
            _session = session;
        }

        public void Create(TestTarget item)
        {
            item.DateCreated = DateTime.Now;
            item.DateModified = DateTime.Now;

            _session.Save(item);
        }

        public IList<TestTarget> GetAll()
        {
            return _session.CreateCriteria(typeof(TestTarget)).List<TestTarget>();
        }

        public TestTarget GetByName(string name)
        {
            return _session.CreateCriteria(typeof(TestTarget)).Add(
                 Restrictions.Eq("Name", name)).UniqueResult<TestTarget>();
        }
    }
}
