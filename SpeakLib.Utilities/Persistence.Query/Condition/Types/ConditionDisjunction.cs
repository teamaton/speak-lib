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

		public override ICriterion GetCriterion()
		{
			if (Items.Count == 0)
				return null;

			return Restrictions.InG(PropertyName, Items);
		}

		public override ICriterion GetCriterion(T item)
		{
			throw new NotImplementedException();
		}

		protected override Junction GetInitializedJunction()
		{
			throw new NotImplementedException();
		}
	}
}