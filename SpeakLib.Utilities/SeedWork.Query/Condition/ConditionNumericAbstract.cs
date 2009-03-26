using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace SpeakFriend.Utilities
{
    public abstract class ConditionNumericAbstract : Condition, IConditionNumeric
    {
        private ConditionComparisonType _queryType;

        public ConditionNumericAbstract(ConditionContainer conditions, string propertyName)
            : base(conditions)
        {
            PropertyName = propertyName;
        }

        public ConditionNumericAbstract(ConditionContainer conditions) : base(conditions){}

        public virtual object GetValue() { throw new NotImplementedException(); }
        public abstract bool IsSet();

        public bool IsGreaterThan()
        {
            return _queryType == ConditionComparisonType.Greater;
        }

        public bool IsEqualTo()
        {
            return _queryType == ConditionComparisonType.Equal;
        }

        protected void SetQueryGreater()
        {
            _queryType = ConditionComparisonType.Greater;
        }

        protected void SetQueryLess()
        {
            _queryType = ConditionComparisonType.Less;
        }

        protected void SetQueryEqual()
        {
            _queryType = ConditionComparisonType.Equal;
        }

        protected bool RemoveIfMinusOne(object value)
        {
            if (Convert.ToInt32(value) == -1)
            {
                Conditions.Remove(this);
                return true;
            }
            return false;
        }

        public override void AddToCriteria(ICriteria criteria)
        {
            criteria.Add(GetCriterion());
        }

        public override ICriterion GetCriterion()
        {
            if (IsEqualTo())
                return Restrictions.Eq(PropertyName, GetValue());
            else if (IsGreaterThan())
                return Restrictions.Gt(PropertyName, GetValue());
            else
                return Restrictions.Lt(PropertyName, GetValue());
        }
    }
}
