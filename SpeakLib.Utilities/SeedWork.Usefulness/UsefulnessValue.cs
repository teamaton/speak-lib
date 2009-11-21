using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities.Usefulness
{
	public class UsefulnessValue
	{
		public uint Positive { get; private set; }
		public uint Negative { get; private set; }

		public UsefulnessValue(uint initialPositive, uint initialNegative)
		{
			Positive = initialPositive;
			Negative = initialNegative;
		}

		public uint Count { get { return Positive + Negative; } }
	}
}
