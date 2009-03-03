﻿using System;
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

        /// <summary>
        /// Adds the given class to the CssClass member of the given control.
        /// </summary>
        public static void AddCssClass(this WebControl control, string classToAdd)
        {
            if (ContainsCssClass(control.CssClass, classToAdd))
                return;

            control.CssClass = GetNewCssString(control.CssClass, classToAdd);
        }

        /// <summary>
        /// Adds the given class to the class attribute of the given html control.
        /// </summary>
        public static void AddCssClass(this HtmlControl control, string classToAdd)
        {
            if (ContainsCssClass(control.Attributes["class"], classToAdd))
                return;

            control.Attributes["class"] = GetNewCssString(control.Attributes["class"], classToAdd);
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
