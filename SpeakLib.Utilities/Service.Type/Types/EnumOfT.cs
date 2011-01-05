using System;
using System.Collections.Generic;
using System.Linq;
using SpeakFriend.Utilities;

namespace SpeakFriend.Utilities
{
	[Serializable]
	public class Enum<T>
	{
		public static IEnumerable<T> GetValues()
		{
			return Enum.GetValues(typeof (T)).OfType<T>();
		}

		public static IEnumerable<string> GetNames()
		{
			return Enum.GetNames(typeof (T));
		}

		/// <summary>
		/// Parses for the name or the value. Ignores the case of the enum string.
		/// </summary>
		public static T Parse(string value)
		{
			return (T) Enum.Parse(typeof (T), value, true);
		}

		/// <summary>
		/// Ignores the case of the enum string.
		/// </summary>
		public static bool TryParse(string value, out T tab)
		{
			try
			{
				tab = Parse(value);
				return true;
			}
			catch (ArgumentException)
			{
				tab = default(T);
				return false;
			}
		}

		public static T Parse(int value)
		{
			return (T) Enum.Parse(typeof (T), value.ToString());
		}

		public static int ToInt(T value)
		{
			return Convert.ToInt32(value);
		}

		public static string ToIntString(T value)
		{
			return ToInt(value).ToString();
		}

		public static string ParseToIntString(string value)
		{
			return ToIntString(Parse(value));
		}
	}
}