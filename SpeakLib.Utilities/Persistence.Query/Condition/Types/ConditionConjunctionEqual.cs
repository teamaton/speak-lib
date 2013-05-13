using System;
using NHibernate.Criterion;

namespace SpeakFriend.Utilities
{
	[Serializable]
	public class ConditionConjunctionEqual<T> : ConditionList<T>
	{
		public ConditionConjunctionEqual(ConditionContainer conditions, string propertyName) : base(conditions, propertyName)
		{
		}

		public override ICriterion GetCriterion(T item)
		{
			return Restrictions.Eq(PropertyName, item);
		}

		protected override Junction GetInitializedJunction()
		{
			return new Conjunction();
		}
	}
}