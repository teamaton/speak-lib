using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;

namespace SpeakFriend.Utilities.Usefulness
{
	public class UsefulnessService : RepositoryDb<UsefulnessEntry, UsefulnessEntryList>
	{
		public UsefulnessService(ISession session) : base(session)
		{
		}

		public UsefulnessValue GetUsefulnessValueByEntity(IUsefulnessEntity entity)
		{
			var result = _session.CreateCriteria(typeof (UsefulnessEntry))
				.Add(Restrictions.Eq("EntityId", entity.Id))
				.Add(Restrictions.Eq("EntityType", entity.Type))
				.SetProjection(Projections.Sum("PositiveValue").As("Positive"),
				               Projections.Sum("NegativeValue").As("Negative"))
				.SetResultTransformer(Transformers.AliasToBean(typeof (UsefulnessValue)))
				.UniqueResult<UsefulnessValue>();

			return result;
		}

		public UsefulnessEntityList GetUsefulnessEntitiesByCreator(IUsefulnessCreator creator)
		{
			var criteria = _session.CreateCriteria(typeof (UsefulnessEntry))
				.Add(Restrictions.Eq("CreatorId", creator.Id))
				.Add(Restrictions.Eq("CreatorType", creator.Type));

			if(creator is UsefulnessCreatorAnonymous)
				criteria.Add(Restrictions.Eq("IpAddress", ((UsefulnessCreatorAnonymous) creator).IpAddress));

			var entries = criteria.List<UsefulnessEntry>();
			var entityList = new UsefulnessEntityList();

			foreach (var entry in entries)
				entityList.Add(new UsefulnessEntityProxy(entry));

			return entityList;
		}
	}
}
