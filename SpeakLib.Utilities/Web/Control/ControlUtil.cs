using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities.Web
{
    internal class ControlUtil
    {
        /// <summary>
        /// Tests whether <paramref name="cssString"/> contains the class <paramref name="cssClass"/>.
        /// </summary>
        /// <param name="cssString"></param>
        /// <param name="cssClass"></param>
        /// <returns></returns>
        public static bool ContainsCssClass(string cssString, string cssClass)
        {
            if (String.IsNullOrEmpty(cssString))
                return false;

            if (cssString.Equals(cssClass)
                || cssString.StartsWith(cssClass + " ")
                || cssString.EndsWith(" " + cssClass)
                || cssString.Contains(" " + cssClass + " "))
                return true;

            return false;
        }

        /// <summary>
        /// Returns the new CSS class string from the old class string and the class to add.
        /// </summary>
        /// <param name="cssString">May be null or empty.</param>
        /// <param name="classToAdd"></param>
        /// <returns></returns>
        public static string GetNewCssString(string cssString, string classToAdd)
        {
            if (string.IsNullOrEmpty(cssString))
                return classToAdd;

            return cssString + " " + classToAdd;
        }
    }
}
