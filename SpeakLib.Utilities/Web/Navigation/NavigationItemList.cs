using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SpeakFriend.Utilities.Web
{
    public class NavigationItemList<T> : List<NavigationItem<T>> where T : HtmlControl
    {
        public void Add(T anchor, params string[] pagesNames)
        {
            foreach (var pageName in pagesNames)
                Add(anchor, pageName);
        }

        /// <summary>
        /// Assigns an HtmlAnchor to a asp.net page name
        /// </summary>
        /// <param name="anchor"></param>
        /// <param name="pageName">The name displayed int the url of the browser</param>
        public void Add(T anchor, string pageName)
        {
            Add(new NavigationItem<T>(anchor, pageName));
        }


        public void Add(T anchor, T anchorParent, string pageName)
        {
            Add(new NavigationItem<T>(anchor, anchorParent, pageName));
        }

        public void Add(T anchor, string pageName, Func<bool> condition)
        {
            Add(new NavigationItem<T>(anchor, pageName, condition));
        }
    }
}
