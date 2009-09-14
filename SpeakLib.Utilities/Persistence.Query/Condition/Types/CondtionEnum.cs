using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace SpeakFriend.Utilities
{
    public class ConditionEnum : Condition
    {
        private Enum _value;
		public Enum Value { get { return _value; } }

        public ConditionEnum(ConditionContainer conditions, string propertyName) : base(conditions, propertyName)
        {
        }

        public void EqualTo(Enum value)
        {
            _value = value;
            AddUniqueToContainer();
        }

        public override void AddToCriteria(ICriteria criteria)
        {
            criteria.Add(GetCriterion());
        }

        public override ICriterion GetCriterion()
        {
            return Restrictions.Eq(PropertyName, _value);
        }

        public override void Reset()
        {
            _value = null;
            base.Reset();
        }
    }

}
