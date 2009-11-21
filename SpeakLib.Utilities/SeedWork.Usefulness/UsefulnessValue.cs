using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities.Usefulness
{
	public class UsefulnessValue
	{
		public int Positive { get; private set; }
		public int Negative { get; private set; }

		public UsefulnessValue(){}

		public UsefulnessValue(int initialPositive, int initialNegative)
		{
			Positive = initialPositive;
			Negative = initialNegative;
		}

		public int Count { get { return Convert.ToInt32(Positive + Math.Abs(Negative)); } }
	}
}
