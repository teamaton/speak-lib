using System;
using NHibernate;
using NHibernate.Criterion;
using SpeakFriend.Utilities;

namespace SpeakFriend.Utilities
{
	[Serializable]
	public class ConditionObject<T> : Condition
	{
		private T _value;
		private bool? _null;

		public ConditionObject(ConditionContainer conditions) : base(conditions)
		{
		}

		public ConditionObject(ConditionContainer conditions, string propertyName) : base(conditions, propertyName)
		{
		}

		public void EqualTo(T value)
		{
			_value = value;
			AddUniqueToContainer();
		}

		public void NotNull()
		{
			_null = false;
			AddUniqueToContainer();
		}

		public override void AddToCriteria(ICriteria criteria)
		{
			criteria.Add(GetCriterion());
		}

		public override ICriterion GetCriterion()
		{
			return _null.HasValue
			       	? _null == false
			       	  	? Restrictions.IsNotNull(PropertyName)
			       	  	: Restrictions.IsNull(PropertyName)
			       	: Restrictions.Eq(PropertyName, _value);
		}
	}
}