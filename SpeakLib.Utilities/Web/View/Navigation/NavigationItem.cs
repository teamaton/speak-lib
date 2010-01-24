using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SpeakFriend.Utilities.Web
{
    public class NavigationItem<T> where T : HtmlControl
    {
        public T Parent;
        public T Anchor;
        public List<string> PageNames = new List<string>();
        public Func<bool> Condition;

        public NavigationItem(T anchor, T parent, string pageName)
        {
            Parent = parent;
            Anchor = anchor;
            PageNames.Add(pageName);
        }

        public NavigationItem(T anchor, string pageName, Func<bool> condition)
        {
            Anchor = anchor;
            PageNames.Add(pageName);
            Condition = condition;
        }

        public NavigationItem(T anchor, params string[] pageNames)
        {
            Anchor = anchor;
            PageNames.AddRange(pageNames);
        }

        public NavigationItem(T anchor, string pageName)
        {
            Anchor = anchor;
            PageNames.Add(pageName);
        }

        public bool IsAssignedToPage(string pageName)
        {
            if (Condition != null && !Condition())
                return false;

            foreach (string currPageName in PageNames)
                if (currPageName.ToLower() == pageName.ToLower())
                    return true;

            return false;
        }
    }
}
