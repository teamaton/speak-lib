using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpeakFriend.Utilities.SeedWork.Usefulness;

namespace SpeakFriend.Utilities.Usefulness
{
	[Serializable]
	public class UsefulnessValue
	{
		public UsefulnessValue()
		{
			
		}

		public UsefulnessValue(int initialPositive, int initialNegative)
		{
			Positive = initialPositive;
			Negative = initialNegative;
		}

		public int Positive { get; private set; }

		public int Negative { get; private set; }

		public int Count { get { return Convert.ToInt32(Positive + Math.Abs(Negative)); } }

		public double Percentage { get { return Count > 0 ? Positive / (double) Count : 0; } }
	}
}
