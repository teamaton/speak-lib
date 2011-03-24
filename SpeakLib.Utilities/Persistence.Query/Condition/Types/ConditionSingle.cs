using System;
using SpeakFriend.Utilities;

namespace SpeakFriend.Utilities
{
	[Serializable]
	public class ConditionSingle : ConditionNumericAbstract	
	{
		private const Single _noValue = -1;
		private Single _value = _noValue;
		private readonly Single _criticalValue = _noValue;

		public ConditionSingle(ConditionContainer conditions, string propertyName)
			: base(conditions)
		{
			PropertyName = propertyName;
		}

		public ConditionSingle(ConditionContainer conditions, string propertyName, Single criticalValue)
			: this(conditions, propertyName)
		{
			_criticalValue = criticalValue;
		}

		public void GreaterThan(object value)
		{
			GreaterThan(Convert.ToSingle(value));
		}

		public void GreaterThan(Single value)
		{
			SetQueryGreater();
			_value = value;

			if (_value == _noValue)
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

		public void LessThanOrEqual(bool isChecked, Single value)
		{
			if (isChecked)
				LessThanOrEqual(value);
			else
				Conditions.Remove(this);
		}

		public void LessThanOrEqual(object value)
		{
			LessThanOrEqual(Convert.ToSingle(value));
		}

		public void LessThanOrEqual(Single value)
		{
			SetQueryLessOrEqual();
			_value = value;

			if (_value == _noValue)
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
			return _value != _noValue;
		}

		public override ConditionNumericAbstract SetThresholdComparison(ConditionComparisonType comparisonType)
		{
			if (_criticalValue == _noValue)
				throw new ArgumentException("Critical value has not been set! Use different constructor.");

			_value = _criticalValue;
			_queryType = comparisonType;
			return this;
		}

		public override void SetActive(bool isChecked)
		{
			if (isChecked)
				Conditions.AddUnique(this);
			else
				Conditions.Remove(this);
		}

		public override void Reset()
		{
			_value = _noValue;
			base.Reset();
		}
	}
}