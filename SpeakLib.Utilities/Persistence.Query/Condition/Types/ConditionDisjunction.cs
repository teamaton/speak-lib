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
			if (typeof (T) != typeof (int))
				return Restrictions.Eq(PropertyName, item);

			throw new InvalidOperationException();
		}

		public override ICriterion GetCriterion()
		{
			if (typeof(T) != typeof(int))
				return base.GetCriterion();

			return Restrictions.InG(PropertyName, Items);
		}

		protected override Junction GetInitializedJunction()
		{
			if (typeof(T) != typeof(int))
				return new Disjunction();

			throw new InvalidOperationException();
		}
	}
}