using System;
using NHibernate;
using NHibernate.Criterion;

namespace SpeakFriend.Utilities
{
    public class ConditionDateTime : Condition
    {
        public ConditionDateTime(ConditionContainer conditions) : base(conditions)
        {
        }

        public ConditionDateTime(ConditionContainer conditions, string propertyName) : base(conditions, propertyName)
        {
        }

        private DateTime? _before;
        private DateTime? _after;

        public override void AddToCriteria(ICriteria criteria)
        {
            var criterion = GetCriterion();
            if (criterion != null) criteria.Add(criterion);
        }

        public override ICriterion GetCriterion()
        {
            if (_after != null && _before != null)
                return Restrictions.Between(PropertyName, _after.Value, _before.Value);

            if (_after != null && _before == null)
                return Restrictions.Gt(PropertyName, _after.Value);

            if (_after == null && _before != null)
                return Restrictions.Lt(PropertyName, _before.Value);

            return null;
        }

        public void After(DateTime time)
        {
            _after = time;
            _before = null;

            Conditions.AddUnique(this);
        }

        public void Before(DateTime time)
        {
            _after = null;
            _before = time;

            Conditions.AddUnique(this);
        }

        public void Between(DateTime time1, DateTime time2)
        {
            if (time1 < time2)
            {
                _after = time1;
                _before = time2;
            }
            else
            {
                _after = time2;
                _before = time1;
            }

            Conditions.AddUnique(this);
        }

        public void Year(int year)
        {
            _after = new DateTime(year, 1, 1);
            _before = new DateTime(year + 1, 1, 1);

            Conditions.AddUnique(this);
        }

        public override void Reset()
        {
            _after = null;
            _before = null;
            base.Reset();
        }
    }
}