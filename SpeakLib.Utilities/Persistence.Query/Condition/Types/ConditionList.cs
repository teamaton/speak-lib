using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Criterion;
using SpeakFriend.Utilities;

namespace SpeakFriend.Utilities
{
	[Serializable]
	public abstract class ConditionList<T> : Condition
	{
		public int ItemCount
		{
			get { return _items.Count; }
		}

		private readonly List<T> _items = new List<T>();

		public List<T> Items
		{
			get { return _items; }
		}

		public ConditionList(ConditionContainer conditions, string propertyName) : base(conditions, propertyName)
		{
		}

		public void Set(List<T> items)
		{
			Clear();
			Add(items);
			if (!Conditions.Contains(this) && _items.Count > 0)
				Conditions.Add(this);
		}

		public void Add(params T[] values)
		{
			foreach (var value in values)
				Add(value);
			if (!Conditions.Contains(this) && _items.Count > 0)
				Conditions.Add(this);
		}

		public void Add(List<T> values)
		{
			foreach (var value in values)
				Add(value);
			if (!Conditions.Contains(this) && _items.Count > 0)
				Conditions.Add(this);
		}

		public void Add(T value)
		{
			ValidateType();

			if (!_items.Contains(value))
				_items.Add(value);
			if (!Conditions.Contains(this) && _items.Count > 0)
				Conditions.Add(this);
		}

		public void Remove(T value)
		{
			ValidateType();

			_items.Remove(value);
			if (!_items.Any())
				Conditions.Remove(this);
		}

		private void ValidateType()
		{
			if (typeof (T) != typeof (Int32) &&
			    typeof (T) != typeof (String) &&
			    typeof (T) != typeof (bool) &&
			    !typeof (T).IsEnum)
				throw new TypeMismatchException("expected int, string or enum");
		}

		public void Clear()
		{
			_items.Clear();
		}

		public override void AddToCriteria(ICriteria criteria)
		{
			var criterion = GetCriterion();
			if (criterion != null) criteria.Add(criterion);
		}

		public override ICriterion GetCriterion()
		{
			if (Items.Count == 0)
				return null;
			var junction = GetInitializedJunction();
			foreach (var item in _items)
				junction.Add(GetCriterion(item));
			return junction;
		}

		public abstract ICriterion GetCriterion(T item);
		protected abstract Junction GetInitializedJunction();

		public override void Reset()
		{
			Items.Clear();
			Conditions.Remove(this);
		}

		public bool Contains(T value)
		{
			ValidateType();

			return _items.Contains(value);
		}
	}
}