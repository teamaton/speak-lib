using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities.Web
{
    using System;
    using System.Web.UI;
    using System.Web.UI.HtmlControls;
    using System.Web.UI.WebControls;

    public class ItemTemplateHelper
    {
        private readonly RepeaterItem _item;

        /// <summary>
        /// True if ListItemType is Item or AlternatingItem
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool IsContentItem(RepeaterItem item)
        {
            return item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem;
        }

        public static bool IsContentItem(DataGridItem item)
        {
            return item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem;
        }

        public static bool IsContentItem(DataListItem item)
        {
            return item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem;
        }

        public static bool IsHeaderItem(RepeaterItem item)
        {
            return item.ItemType == ListItemType.Header;
        }

        public static bool IsAlternatingItem(RepeaterItem item)
        {
            return item.ItemType == ListItemType.AlternatingItem;
        }

        public ItemTemplateHelper(RepeaterItem item)
        {
            _item = item;
        }

        private static void ThrowIfNull(string name, object result)
        {
            if (result == null)
                throw new Exception("The control:'" + name + "' was not found");
        }

        public T Find<T>(string controlName) where T : Control
        {
            T item = (T)_item.FindControl(controlName);
            ThrowIfNull(controlName, item);
            return item;
        }
    }
}
