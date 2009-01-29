using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SpeakFriend.Utilities.Web
{
    public static class WebControlExtensionMethods
    {
        /// <summary>
        /// Returns the CSS with the specified class appended. Checks if the class exists before adding.
        /// </summary>
        public static void AddCssClass(this WebControl control, string classToAdd)
        {
            if (string.IsNullOrEmpty(control.CssClass))
            {
                control.CssClass = classToAdd;
                return;
            }

            if (control.CssClass.Equals(classToAdd)
                || control.CssClass.Contains(classToAdd + " ")
                || control.CssClass.Contains(" " + classToAdd))
                return;

            control.CssClass += " " + classToAdd;
        }

        public static bool IsContentItem(this RepeaterItem item)
        {
            return item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem;
        }

        public static bool IsContentItem(this DataGridItem item)
        {
            return item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem;
        }

        public static bool IsContentItem(this DataListItem item)
        {
            return item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem;
        }

        public static bool IsHeaderItem(this RepeaterItem item)
        {
            return item.ItemType == ListItemType.Header;
        }

        public static bool IsAlternatingItem(this RepeaterItem item)
        {
            return item.ItemType == ListItemType.AlternatingItem;
        }

        public static T Find<T>(this RepeaterItem repeaterItem, string controlName) where T : Control
        {
            T item = (T)repeaterItem.FindControl(controlName);
            if (item == null)
                throw new Exception("The controlName:'" + controlName + "' was not found");
            return item;
        }

    }
}
