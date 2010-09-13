using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities
{
	public static class DateTimeExtensions
	{
		private static readonly DateTime DateTime01011970 = new DateTime(1970, 1, 1);

		/// <summary>
		/// Returns the number of milliseconds that elapsed from 01/01/1970 to the given <paramref name="dateTime"/>.
		/// </summary>
		public static long ToMilliseconds(this DateTime dateTime)
		{
			return Convert.ToInt64((dateTime - DateTime01011970).TotalMilliseconds);
		}
	}
}
