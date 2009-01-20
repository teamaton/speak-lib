using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;

namespace SpeakFriend.Utilities.Tagging
{
    public class TagService 
    {
        private readonly ISession _session;

        public TagService(ISession session)
        {
            _session = session;
        }

        public IList<Tag> GetByItem(ITaggable item)
        {
            var id = item.Id;
            var typeName = item.GetType().AssemblyQualifiedName;

            DetachedCriteria query =
                    DetachedCriteria.For(typeof(Tag), "t").
                    CreateCriteria("Prototype", "p", JoinType.InnerJoin);
                    
            ICriteria criteriaQuery = query.GetExecutableCriteria(_session);

            criteriaQuery.Add(Restrictions.Eq("p.TargetTypeName", typeName));
            criteriaQuery.Add(Restrictions.Eq("t.TargetId", id));

            return criteriaQuery.List<Tag>();

        }
        
        public void Create(Tag tag)
        {
            tag.DateCreated = DateTime.Now;
            tag.DateModified = DateTime.Now;

            _session.Save(tag);
        }
    }
}