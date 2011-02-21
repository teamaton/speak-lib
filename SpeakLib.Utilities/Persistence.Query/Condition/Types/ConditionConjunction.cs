using System;
using NHibernate.Criterion;
using SpeakFriend.Utilities;

namespace SpeakFriend.Utilities
{
	[Serializable]
	public class ConditionConjunction<T> : ConditionList<T>
	{
		public ConditionConjunction(ConditionContainer conditions, string propertyName)
			: base(conditions, propertyName)
		{
		}

		protected override Junction GetInitializedJunction()
		{
			return new Conjunction();
		}

		public override ICriterion GetCriterion(T item)
		{
			return Restrictions.Eq(PropertyName, item);
		}
	}
}