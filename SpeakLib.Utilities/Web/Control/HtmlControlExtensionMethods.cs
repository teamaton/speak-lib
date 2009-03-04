using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.HtmlControls;

namespace SpeakFriend.Utilities.Web
{
    public static class HtmlControlExtensionMethods
    {
        /// <summary>
        /// Returns the CSS with the specified class appended. Checks if the class exists before adding.
        /// </summary>
        public static void AddCssClass(this HtmlControl control, string classToAdd)
        {
            if (string.IsNullOrEmpty(control.Attributes["class"]))
            {
                control.Attributes["class"] = classToAdd;
                return;
            }

            if (control.Attributes["class"].Equals(classToAdd)
                || control.Attributes["class"].Contains(classToAdd + " ")
                || control.Attributes["class"].Contains(" " + classToAdd))
                return;

            control.Attributes["class"] += " " + classToAdd;
        }
    }
}
