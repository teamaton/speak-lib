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

		/// <summary>
		/// Returns a list of all usefulness entries for the given creator; in case of a
		/// <see cref="UsefulnessCreatorAnonymous"/> the search is done based on IP address and a time span.
		/// </summary>
		public UsefulnessEntityList GetUsefulnessEntitiesByCreator(IUsefulnessCreator creator)
		{
			var criteria = _session.CreateCriteria(typeof (UsefulnessEntry))
				.Add(Restrictions.Eq("CreatorId", creator.Id))
				.Add(Restrictions.Eq("CreatorType", creator.TypeName));

			if (creator is UsefulnessCreatorAnonymous)
			{
				var anonymous = (UsefulnessCreatorAnonymous) creator;
				criteria.Add(Restrictions.Eq("IpAddress", anonymous.IpAddress));
				if (anonymous.BlockingPeriod.HasValue)
					criteria.Add(Restrictions.Ge("DateCreated", DateTime.Now.Add(-anonymous.BlockingPeriod.Value)));
			}

			var entries = criteria.List<UsefulnessEntry>();
			var entityList = new UsefulnessEntityList();

			foreach (var entry in entries)
				entityList.Add(new UsefulnessEntityProxy(entry));

			return entityList;
		}

		internal ISession Session { get { return _session; } }

		public void FillWithUsefulnessValue(UsefulnessEntityProxy entity)
		{
			var value = GetUsefulnessValueByEntity(entity);
			entity.Usefulness = value;
		}
	}
}
