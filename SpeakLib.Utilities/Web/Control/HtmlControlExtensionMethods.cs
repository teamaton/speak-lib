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
        /// Adds the given class to the class attribute of the given html control.
        /// </summary>
        public static void AddCssClass(this HtmlControl control, string classToAdd)
        {
            if (ExtBase.ContainsCssClass(control.Attributes["class"], classToAdd))
                return;

            control.Attributes["class"] = ExtBase.GetNewCssString(control.Attributes["class"], classToAdd);
        }
    }
}
