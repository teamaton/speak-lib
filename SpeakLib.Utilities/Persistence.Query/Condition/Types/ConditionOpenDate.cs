using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace SpeakFriend.Utilities
{
	[Serializable]
	public class ConditionOpenDate : Condition
	{
		private DateTime _value;


		public ConditionOpenDate(ConditionContainer conditions) : base(conditions, "_openDate")
		{
		}

		public ConditionOpenDate(ConditionContainer conditions, string propertyName) : base(conditions, propertyName)
		{
			throw new NotImplementedException();
		}

		public void SetDate(DateTime value)
		{
			_value = new DateTime(1900, value.Month, value.Day);
			Conditions.AddUnique(this);
		}

		public DateTime GetDate()
		{
			return _value;
		}

		public override void AddToCriteria(ICriteria criteria)
		{
			criteria.Add(GetCriterion());
		}

		public override ICriterion GetCriterion()
		{
			var summer = Restrictions.Conjunction().Add(Restrictions.Le("OpeningHoursSummerBegin", _value)).Add(Restrictions.Ge("OpeningHoursSummerEnd", _value));
			var winter = Restrictions.Disjunction().Add(Restrictions.Le("OpeningHoursWinterBegin", _value)).Add(Restrictions.Ge("OpeningHoursWinterEnd", _value));
			return Restrictions.Disjunction().Add(summer).Add(winter);
		}
	}
}
