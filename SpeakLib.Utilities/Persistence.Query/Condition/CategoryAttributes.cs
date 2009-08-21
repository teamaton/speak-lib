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

	public interface ICategoryNumeric
	{
		object Value { get; }
	}

	public abstract class CategoryBaseAttribute : Attribute
	{
	}

	public class CategoryBooleanAttribute : CategoryBaseAttribute
    {
    }

	public class CategoryIntegerAttribute : CategoryBaseAttribute, ICategoryNumeric
    {
		public int Value { get; private set; }

		object ICategoryNumeric.Value
		{
			get { return Value; }
		}
		
		public CategoryIntegerAttribute(int value)
		{
			Value = value;
		}
    }

	public class CategorySingleAttribute : CategoryBaseAttribute, ICategoryNumeric
    {
		public Single Value { get; private set; }

		object ICategoryNumeric.Value
		{
			get { return Value; }
		}

		public CategorySingleAttribute(Single value)
		{
			Value = value;
		}
    }

	public class CategoryDoubleAttribute : CategoryBaseAttribute, ICategoryNumeric
    {
		public double Value { get; private set; }

		object ICategoryNumeric.Value
		{
			get { return Value; }
		}
		
		public CategoryDoubleAttribute(double value)
		{
			Value = value;
		}
    }
}
