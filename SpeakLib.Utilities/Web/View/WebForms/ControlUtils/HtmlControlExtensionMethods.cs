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
            if (ControlUtil.ContainsCssClass(control.Attributes["class"], classToAdd))
                return;

            control.Attributes["class"] = ControlUtil.GetNewCssString(control.Attributes["class"], classToAdd);
        }

		/// <summary>
		/// Sets the given class(es) as the class attribute of the given html control.
		/// </summary>
		public static void SetCssClass(this HtmlControl control, params string[] classesToAdd)
		{
			control.ResetCssClass();

			foreach (var classToAdd in classesToAdd)
				control.AddCssClass(classToAdd);
		}

    	public static string ResetCssClass(this HtmlControl control)
    	{
    		return control.Attributes["class"] = string.Empty;
    	}

    	public static bool ContainsCssClass(this HtmlControl control, string cssClass)
		{
			return ControlUtil.ContainsCssClass(control.Attributes["class"], cssClass);
		}
    }
}
