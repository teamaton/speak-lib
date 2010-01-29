using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;

namespace SpeakFriend.Utilities
{
	/// <summary>
	/// Tightly coupled to Generic.Core AffiliateLink functionality, but here for inspiration :-)
	/// </summary>
	/// <typeparam name="TParent">The type of the entity the query is built for.</typeparam>
	/// <typeparam name="TChild">The type of the referenced entity.</typeparam>
	public class ConditionHasAffiliateLink<TParent, TChild> : ConditionObject<TChild>
	{
		private readonly List<int> _affiliateLinkIds = new List<int>();
		protected string Alias { get; set; }

		public ConditionHasAffiliateLink(ConditionContainer conditions) : base(conditions)
		{
		}

		public ConditionHasAffiliateLink(ConditionContainer conditions, string propertyName)
			: base(conditions, propertyName)
		{
		}

		public ConditionHasAffiliateLink<TParent, TChild> Has(params int[] affiliateLinkIds)
		{
			_affiliateLinkIds.AddRange(affiliateLinkIds);
			AddUniqueToContainer();
			return this;
		}

		public override void AddToCriteria(ICriteria criteria)
		{
			Alias = criteria.Alias;
			criteria.Add(GetCriterion());
		}

		public override ICriterion GetCriterion()
		{
			var detachedCriteria = DetachedCriteria.For<TChild>("CAL");
			detachedCriteria.Add(Restrictions.EqProperty("GeoObject.Id", Alias + ".Id"))
				.Add(Restrictions.InG("AffiliateLink.Id", _affiliateLinkIds))
				.SetProjection(Projections.CountDistinct("AffiliateLink.Id"));

			return Restrictions.Eq(Projections.SubQuery(detachedCriteria), _affiliateLinkIds.Count);
		}
	}
}