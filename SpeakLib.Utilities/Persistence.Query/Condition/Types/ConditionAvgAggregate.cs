using System;
using NHibernate;
using NHibernate.Criterion;
using SpeakFriend.Utilities;

namespace SpeakFriend.Utilities
{
	[Serializable]
	public class ConditionAvgAggregate : ConditionDouble
	{
		public ConditionAvgAggregate(ConditionContainer conditions, string propertyName)
			: base(conditions, propertyName)
		{
		}

		public override void AddToCriteria(ICriteria criteria)
		{
			if (IsGreaterThan())
				criteria.Add(Restrictions.Gt(_Projections.EmptyGroupDoubleAvg(PropertyName), GetValue()));
			else
				criteria.Add(Restrictions.Lt(_Projections.EmptyGroupDoubleAvg(PropertyName), GetValue()));
		}
	}
}