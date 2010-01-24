using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities.Web
{
	public class TagCloud
	{
		public int MinValue { get; private set; }
		public int MaxValue { get; private set; }

		public List<string> CssClasses = new List<string>();
		public int CssClassCount { get { return CssClasses.Count; } }

		public TagCloud(int minValue, int maxValue, params string[] cssClasses)
		{
			if (MaxValue < MinValue)
				throw new ArgumentException("maxValue must be larger than minValue!", "maxValue");
			if (cssClasses == null || cssClasses.Count() < 1)
				throw new ArgumentException("cssClasses must not be null and must contain at least" +
				                            " one value!", "cssClasses");

			MinValue = minValue;
			MaxValue = maxValue;
			CssClasses.AddRange(cssClasses);
		}

		public string GetCssClass(int value)
		{
			decimal step = (MaxValue - MinValue) / (decimal) CssClassCount;
			decimal lower = MinValue;
			int slot = 0;
			while (lower + step < value)
			{
				lower += step;
				slot++;
			}
			return CssClasses[slot];
		}
	}
}
