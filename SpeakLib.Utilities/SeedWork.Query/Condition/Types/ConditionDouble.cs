using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Criterion;

namespace SpeakFriend.Utilities
{
    public class ConditionDouble : ConditionNumericAbstract, IConditionNumeric
    {
        private double _value = -1;

        public ConditionDouble(ConditionContainer conditions, string propertyName)
            : base(conditions, propertyName)
        {
            PropertyName = propertyName;
        }

        public void GreaterThan(object value)
        {
            GreaterThan(Convert.ToDouble(value));
        }

        public void GreaterThan(double value)
        {
            SetQueryGreater();
            _value = value;

            if (_value == -1)
            {
                Conditions.Remove(this);
                return;
            }

            Conditions.AddUnique(this);
        }

        public void GreaterThan(bool isChecked, double value)
        {
            if (isChecked)
                GreaterThan(value);
            else
                Conditions.Remove(this);
        }

        public void LessThan(bool isChecked, double value)
        {
            if (isChecked)
                LessThan(value);
            else
                Conditions.Remove(this);
        }

        public void LessThan(object value)
        {
            LessThan(Convert.ToDouble(value));
        }

        public void LessThan(double value)
        {
            _value = value;
            SetQueryLess();

            if (_value == -1)
            {
                Conditions.Remove(this);
                return;
            }

            Conditions.AddUnique(this);
        }

        public void EqualTo(double value)
        {
            SetQueryEqual();
            _value = value;
        }

        public override object GetValue()
        {
            return _value;
        }

        public override bool IsSet()
        {
            return _value != -1;
        }

        /// <summary>
        /// Checks whether this condition is set and contained in the ConditionList.
        /// </summary>
        /// <returns>True if this condition is contained in the ConditionList AND if 
        /// its value is set to something other than the default, else false.</returns>
        public bool IsActive()
        {
            return IsSet() && Conditions.Contains(this);
        }
    }
}
