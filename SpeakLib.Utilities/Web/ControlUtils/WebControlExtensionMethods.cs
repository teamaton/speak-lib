using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SpeakFriend.Utilities.Web
{
    public static class WebControlExtensionMethods
    {
        /// <summary>
        /// Adds the given class to the CssClass member of the given control.
        /// </summary>
        public static void AddCssClass(this WebControl control, string classToAdd)
        {
            if (ControlUtil.ContainsCssClass(control.CssClass, classToAdd))
                return;

            control.CssClass = ControlUtil.GetNewCssString(control.CssClass, classToAdd);
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

        public static T Find<T>(this Control control, string controlName) where T : Control
        {
            var item = (T)control.FindControl(controlName);
            if (item == null)
                throw new Exception("The controlName:'" + controlName + "' was not found");
            return item;
        }
    }
}
