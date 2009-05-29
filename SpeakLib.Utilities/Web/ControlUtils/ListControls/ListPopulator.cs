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
            var date = new DateTime(1, 1, 1); // used only for the month

            for (int i = 0; i < 12; i++)
            {
                listControl.Items.Add(new ListItem(
                    date.ToString("MMMM", cultureInfo.NumberFormat), // full month name as TEXT
                    date.ToString("%M"))); // e.g. "9" as ListItem.Value for September

                date = date.AddMonths(1);
            }
        }
    }
}
