using System;
using NHibernate;
using NHibernate.Criterion;

namespace SpeakFriend.Utilities
{
	/// <summary>
	/// Call AsNullable() on a newly generated ConditionBoolean to get a Condition for a nullable boolean.
	/// </summary>
	[Serializable]
	public class ConditionBoolean : Condition
	{
		private bool? _value = false;
		private bool _nullable = false;

		public ConditionBoolean(ConditionContainer conditions, string propertyName)
			: base(conditions)
		{
			PropertyName = propertyName;
		}

		public ConditionBoolean AsNullable()
		{
			_nullable = true;
			return this;
		}

		/// <summary>
		/// Sets the condition to [true AND active] or [false AND inactive].
		/// </summary>
		/// <param name="value"></param>
		public void SetTrueOrInactive(bool value)
		{
			if (value)
				SetTrue();
			else
			{
				_value = false;
				Conditions.Remove(this);
			}
		}

		public bool IsTrue()
		{
			return _value==true;
		}

		/// <summary>
		/// Sets the condition to true
		/// </summary>
		public void SetTrue()
		{
			_value = true;
			Conditions.AddUnique(this);
		}

		public bool IsFalse()
		{
			return _value==false;
		}

		/// <summary>
		/// Sets the condition to false (and leaves it ACTIVE!).
		/// </summary>
		public void SetFalse()
		{
			_value = false;
			Conditions.AddUnique(this);
		}

		/// <summary>
		/// Sets the condition to false (and leaves it ACTIVE!).
		/// </summary>
		public void SetNull()
		{
			if(!_nullable)
				throw new InvalidOperationException("Call SetNullable(true) on this condition (" + PropertyName +
													") before setting it to NULL!");
			_value = null;
			Conditions.AddUnique(this);
		}

		/// <summary>
		/// Checks whether this condition is set and contained in the ConditionList.
		/// </summary>
		/// <returns>True if this condition is contained in the ConditionList AND if 
		/// its value is set to something other than the default, else false.</returns>
		public override bool IsActive()
		{
			return IsTrue() && Conditions.Contains(this);
		}

		public override void AddToCriteria(ICriteria criteria)
		{
			criteria.Add(GetCriterion());
		}

		public override ICriterion GetCriterion()
		{
			if (!_nullable && _value == null)
				throw new InvalidOperationException("This condition (" + PropertyName +
				                                    ") can only be set to NULL after SetNullable() has been called!");

			return _value.HasValue
			       	? Restrictions.Eq(PropertyName, _value.Value)
			       	: Restrictions.IsNull(PropertyName);
		}

		public override void Reset()
		{
			SetTrueOrInactive(false);
		}
	}
}