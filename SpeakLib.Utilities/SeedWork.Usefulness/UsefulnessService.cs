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

		public UsefulnessValue GetUsefulnessValueByEntity(IUsefulnessEntity usefulEntity)
		{
			var result = _session.CreateCriteria(typeof (UsefulnessEntry))
				.Add(Restrictions.Eq("EntityId", usefulEntity.Id))
				.Add(Restrictions.Eq("EntityType", usefulEntity.GetType().Name))
				.SetProjection(Projections.Sum("PositiveValue").As("Positive"),
				               Projections.Sum("NegativeValue").As("Negative"))
				.SetResultTransformer(Transformers.AliasToBean(typeof (UsefulnessValue)))
				.UniqueResult<UsefulnessValue>();

			return result;
		}
	}
}
