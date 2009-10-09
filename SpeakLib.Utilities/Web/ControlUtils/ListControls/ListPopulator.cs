using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace SpeakFriend.Utilities
{
    public static class ListPopulator
    {
        /// <summary>
        /// Adds all months of the year as <see cref="ListItem"/>s to the given ListControl.
        /// </summary>
        /// <param name="listControl"></param>
        /// <param name="cultureInfo"></param>
        public static void PopulateMonthNames(this ListControl listControl, CultureInfo cultureInfo)
        {
        	listControl.PopulateMonthNames(cultureInfo, false);
        }

    	public static void PopulateMonthNames(this ListControl listControl, CultureInfo cultureInfo, bool shortNames)
    	{
    		var date = new DateTime(1, 1, 1); // used only for the month

    		var format = shortNames ? "MMM" : "MMMM";
            for (int i = 0; i < 12; i++)
            {
                listControl.Items.Add(new ListItem(
                    date.ToString(format, cultureInfo.NumberFormat), // full month name as TEXT
                    date.ToString("%M"))); // e.g. "9" as ListItem.Value for September

                date = date.AddMonths(1);
            }
    	}

    	/// <summary>
        /// Adds all months of the year as <see cref="ListItem"/>s to the given ListControl.
        /// </summary>
        /// <param name="listControl"></param>
        /// <param name="cultureInfo"></param>
        public static void PopulateWeekDayNames(this ListControl listControl, CultureInfo cultureInfo)
        {
			// Use this approach rather than DateTimeFormatInfo.GetInstance(cultureInfo).DayNames
			// because the first day of the week differs between cultures!!!

			var date = new DateTime(2009, 10, 5); // used only for the weekday: 05.10.2009 was a Monday

			// Monday is our first day of the week.
			for (int i = 1; i <= 7; i++)
			{
				listControl.Items.Add(new ListItem(
					date.ToString("dddd", cultureInfo.NumberFormat), // full week day as text
					i.ToString())); 

				date = date.AddDays(1);
			}
        }

    	/// <summary>
		/// Adds the given numbers as <see cref="ListItem"/>s to the given ListControl.
		/// </summary>
		public static void PopulateNumbers(this ListControl listControl, int min, int max, int step)
		{
			listControl.Items.Clear();
			for (int i = min; i <= max; i += step)
				listControl.Items.Add(new ListItem(i.ToString(), i.ToString()));
		}
    }
}
