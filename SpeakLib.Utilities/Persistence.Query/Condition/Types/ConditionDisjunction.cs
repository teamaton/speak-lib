using System;
using NHibernate.Criterion;
using SpeakFriend.Utilities;

namespace SpeakFriend.Utilities
{
	[Serializable]
	public class ConditionDisjunction<T> : ConditionList<T>
	{
		public ConditionDisjunction(ConditionContainer conditions, string propertyName)
			: base(conditions, propertyName)
		{
		}

		public override ICriterion GetCriterion(T item)
		{
			return Restrictions.Eq(PropertyName, item);
		}

		protected override Junction GetInitializedJunction()
		{
			return new Disjunction();
		}
	}
}