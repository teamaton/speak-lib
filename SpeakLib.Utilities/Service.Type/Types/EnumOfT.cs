using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities
{
    public class Enum<T> 
    {
    	public static IList<T> GetValues()
    	{
    		IList<T> list = new List<T>();
    		foreach (object value in Enum.GetValues(typeof(T)))
    		{
    			list.Add((T)value);
    		}
    		return list;
    	}

		/// <summary>
		/// Parses for the name or the value. Ignores the case of the enum string.
		/// </summary>
    	public static T Parse(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
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
			catch(ArgumentException)
			{
				tab = default(T);
				return false;
			}
    	}

    	public static T Parse(int value)
        {
            return (T)Enum.Parse(typeof (T), value.ToString());
        }

		public static int ParseToInt(T value)
		{
			return Convert.ToInt32(value);
		}

		public static string ParseToIntString(T value)
		{
			return ParseToInt(value).ToString();
		}

    	public static string ParseToIntString(string value)
    	{
    		return ParseToIntString(Parse(value));
    	}
    }
}
