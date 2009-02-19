using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SpeakFriend.Utilities.Web
{
    public class NavigationItemService<T> where T : HtmlControl
    {
        readonly NavigationItemList<T> _navItems = new NavigationItemList<T>();
        private readonly string activeItemClass = "current";

        public NavigationItemService(NavigationItemList<T> navItems)
        {
            _navItems = navItems;
        }

        public NavigationItemService(NavigationItemList<T> navItems, string cssClass)
        {
            _navItems = navItems;
            activeItemClass = cssClass;
        }

        public void HighlightActive(Page page)
        {
            string[] fileParts = page.Request.FilePath.Split('/');
            string fileName = fileParts[fileParts.Length - 1];

            HighlightActive(fileName);
        }

        public void HighlightActive(string pageName)
        {
            foreach (var navItem in _navItems)
                if (navItem.IsAssignedToPage(pageName))
                    SetActive(navItem);
                else
                    SetInactive(navItem);
        }

        private void SetActive(NavigationItem<T> navItem)
        {
            navItem.Anchor.Attributes.Add("class", activeItemClass);
            
            if(navItem.Parent != null)
                navItem.Parent.Attributes.Add("class", activeItemClass);
        }

        private void SetInactive(NavigationItem<T> navItem)
        {
            navItem.Anchor.Attributes.Remove("class");
        }
    }
}
