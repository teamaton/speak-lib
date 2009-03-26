using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities
{
    public class ConditionSingle : ConditionNumericAbstract, IConditionNumeric
    {
        private Single _value = -1;

        public ConditionSingle(ConditionContainer conditions, string propertyName)
            : base(conditions)
        {
            PropertyName = propertyName;
        }

        public void GreaterThan(object value)
        {
            GreaterThan(Convert.ToSingle(value));
        }

        public void GreaterThan(Single value)
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

        public void GreaterThan(bool isChecked, Single value)
        {
            if (isChecked)
                GreaterThan(value);
            else
                Conditions.Remove(this);
        }

        public void LessThan(bool isChecked, Single value)
        {
            if (isChecked)
                LessThan(value);
            else
                Conditions.Remove(this);
        }

        public void LessThan(object value)
        {
            LessThan(Convert.ToSingle(value));
        }

        public void LessThan(Single value)
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
