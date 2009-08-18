using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities
{
	public interface ICategoryNumericAttribute
	{
		object Value { get; }
	}

	public abstract class CategoryBaseAttribute : Attribute
	{
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
