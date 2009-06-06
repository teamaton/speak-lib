﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace SpeakFriend.Utilities
{
    public static class PageExtensionMethods
    {
		/// <summary>
		/// Typed version of Master, runs up the MasterPage hierarchy to find the provided
		/// MasterPage type T. Returns null, if no match found.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="page"></param>
		/// <returns></returns>
		public static T Master<T>(this Page page) where T:MasterPage
		{
			var master = page.Master;
			while (master != null && !(master is T))
				master = master.Master;

			return master as T;
		}

        /// <summary>
        /// Add a CSS file reference to the HEAD section of a Page.
        /// </summary>
        /// <param name="page">The Page instance to which to add the CSS file reference.</param>
        /// <param name="cssFileName">The application relative path of the CSS file to add.</param>
        public static void AddStyleSheet(this Page page, string cssFileName)
        {
            var htmlLink = new HtmlLink();
            htmlLink.Href = cssFileName;
            htmlLink.Attributes.Add("rel", "Stylesheet");
            htmlLink.Attributes.Add("type", "text/css");
            htmlLink.Attributes.Add("media", "all");
            page.Header.Controls.Add(htmlLink);
        }
    }
}
