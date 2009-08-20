using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities
{
	public enum Importance
	{
		None = 0,
		Prominent = 1
	}

	[Flags]
	public enum RatingOptionsFlags
	{
		None = 2^0,
		ShowRatingCount = 2^1
	}

	public interface ICategoryNumericAttribute
	{
		object Value { get; }
	}

	public abstract class CategoryBaseAttribute : Attribute
	{
	}

	public class RatingCategoryAttribute : CategoryBaseAttribute
    {
		private readonly Importance _importance = Importance.None;
		private readonly RatingOptionsFlags _ratingOptions = RatingOptionsFlags.None;

		public bool IsProminent { get { return _importance == Importance.Prominent; } }
		public bool ShowRatingCount
		{
			get { return (_ratingOptions & RatingOptionsFlags.ShowRatingCount) == RatingOptionsFlags.ShowRatingCount; }
		}

		public RatingCategoryAttribute(){}
		public RatingCategoryAttribute(Importance importance)
		{
			_importance = importance;
		}
		public RatingCategoryAttribute(Importance importance, RatingOptionsFlags ratingOptions)
			: this(importance)
		{
			_ratingOptions = ratingOptions;
		}
    }

	public class CategoryBooleanAttribute : CategoryBaseAttribute
    {
    }

	public class CategoryIntegerAttribute : CategoryBaseAttribute, ICategoryNumericAttribute
    {
		public int Value { get; private set; }

		object ICategoryNumericAttribute.Value
		{
			get { return Value; }
		}
		
		public CategoryIntegerAttribute(int value)
		{
			Value = value;
		}
    }

	public class CategorySingleAttribute : CategoryBaseAttribute, ICategoryNumericAttribute
    {
		public Single Value { get; private set; }

		object ICategoryNumericAttribute.Value
		{
			get { return Value; }
		}

		public CategorySingleAttribute(Single value)
		{
			Value = value;
		}
    }

	public class CategoryDoubleAttribute : CategoryBaseAttribute, ICategoryNumericAttribute
    {
		public double Value { get; private set; }

		object ICategoryNumericAttribute.Value
		{
			get { return Value; }
		}
		
		public CategoryDoubleAttribute(double value)
		{
			Value = value;
		}
    }
}
