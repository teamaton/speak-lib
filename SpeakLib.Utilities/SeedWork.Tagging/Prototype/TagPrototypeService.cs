using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;

namespace SpeakFriend.Utilities.Tagging
{
    public class TagPrototypeService 
    {
        private readonly ISession _session;

        public TagPrototypeService(ISession session)
        {
            _session = session;
        }

        public void Create(TagPrototype prototype)
        {
            prototype.DateCreated = DateTime.Now;
            prototype.DateModified = DateTime.Now;

            _session.Save(prototype);
        }

        public TagPrototype GetByTargetTypeAndText(Type targetType, string text)
        {
            return GetByTargetTypeAndText(targetType.AssemblyQualifiedName, text);
        }

        public TagPrototype GetByTargetTypeAndText(string targetTypeAssemblyQualifiedName, string text)
        {
            return _session.CreateCriteria(typeof(TagPrototype)).Add(
                 Restrictions.Eq("TargetTypeName", targetTypeAssemblyQualifiedName)).Add(
                 Restrictions.Eq("Text", text)).UniqueResult<TagPrototype>();
        }

        public IList<TagPrototype> GetByTargetType(Type targetType)
        {
            return GetByTargetType(targetType.AssemblyQualifiedName);
        }

        public IList<TagPrototype> GetByTargetType(string targetTypeAssemblyQualifiedName)
        {
           return  _session.CreateCriteria(typeof (TagPrototype)).Add(
                Restrictions.Eq("TargetTypeName", targetTypeAssemblyQualifiedName)).List<TagPrototype>();
        }
    }
}