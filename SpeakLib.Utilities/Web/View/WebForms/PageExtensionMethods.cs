using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using SpeakFriend.Utilities;

namespace SpeakFriend.Utilities.Web
{
	public static class PageExtensionMethods
	{
		/// <summary>
		/// Typed version of Master, runs up the MasterPage hierarchy to find the provided
		/// MasterPage type T. Returns null, if no match found.
		/// </summary>
		public static T Master<T>(this Page page) where T : MasterPage
		{
			var master = page.Master;
			while (master != null && !(master is T))
				master = master.Master;

			return master as T;
		}

		/// <summary>
		/// Returns the most concrete MasterPage Type of this Page that is assignable to the given Type T, or null if none such exists.
		/// <br />
		/// Used to get the concrete subclass type of a known base MasterPage type.
		/// </summary>
		public static T TopMasterOfType<T>(this Page page) where T : MasterPage
		{
			var master = page.Master;
			T result = null;

			while (master != null)
			{
				if (typeof (T).IsAssignableFrom(master.GetType()))
					result = master as T;

				master = master.Master;
			}

			return result;
		}

		/// <summary>
		/// Add a CSS file reference to the HEAD section of a Page.
		/// </summary>
		/// <param name="page">The Page instance to which to add the CSS file reference.</param>
		/// <param name="cssFileName">The application relative path of the CSS file to add.</param>
		public static void AddStyleSheet(this Page page, string cssFileName)
		{
			var htmlLink = new HtmlLink {Href = cssFileName};
			htmlLink.Attributes.Add("rel", "stylesheet");
			htmlLink.Attributes.Add("type", "text/css");
			htmlLink.Attributes.Add("media", "all");
			page.Header.Controls.Add(htmlLink);
		}

		public static void AddCanonicalTag(this Page page, string canonicalUrl)
		{
			var link = new HtmlLink {Href = canonicalUrl};
			link.Attributes.Add("rel", "canonical");
			page.Header.Controls.Add(link);
		}

		public static void AddMetaTag(this Page page, string name, string content)
		{
			page.Header.Controls.Add(new HtmlMeta {Name = name, Content = content});
		}

		public static void AddScriptTag(this Page page, string src)
		{
			AddScriptTag(page, src, false);
		}

		public static void AddScriptTag(this Page page, string src, bool addToBeginning)
		{
			var scriptTag = new HtmlGenericControl("script");
			scriptTag.Attributes.Add("src", src);
			scriptTag.Attributes.Add("type", "text/javascript");
			if (addToBeginning)
				page.Header.Controls.AddAt(0, scriptTag);
			else
				page.Header.Controls.Add(scriptTag);
		}

		/// <summary>
		/// Scrolls the page to the Control with the given ID. Uses jquery.scrollTo.js plugin functionality.
		/// </summary>
		public static void ScrollTo(this Control control, string controlClientId)
		{
			var key = "sl_scroll_to";

			ScriptManager.RegisterStartupScript(
				control, control.GetType(), key,
				string.Format("{0}$(function (){{ $.scrollTo('#" + controlClientId +
				              "', {{ offset:-8, duration: 500 }}); }});{0}", Environment.NewLine),
				true);
		}

		/// <summary>
		/// Scrolls the browser page to the top. Uses jquery.scrollTo.js plugin functionality.
		/// </summary>
		public static void ScrollToTopOfPage(this Control control)
		{
			var key = "sl_scroll_to";

			ScriptManager.RegisterStartupScript(
				control, control.GetType(), key,
				string.Format("{0}$(function (){{ $.scrollTo(0, {{ offset:-8, duration: 500 }}); }});{0}",
				              Environment.NewLine),
				true);
		}
	}
}