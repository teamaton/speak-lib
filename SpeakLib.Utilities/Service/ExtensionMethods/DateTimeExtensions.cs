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

		/// <summary>
		/// Returns a new <see cref="DateTime"/> object with the same date part as the given <paramref name="dateTime"/>
		/// but the time set to 00:00:00.
		/// </summary>
		public static DateTime StartOfDay(this DateTime dateTime)
		{
			return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
		}

		/// <summary>
		/// Returns a new <see cref="DateTime"/> object with the same date part as the given <paramref name="dateTime"/>
		/// but the time set to 23:59:59.999.
		/// </summary>
		public static DateTime EndOfDay(this DateTime dateTime)
		{
			return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59, 999);
		}
	}
}
