using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SpeakFriend.Utilities
{
	/// <summary>
	/// This extended LinkButton offers the possibility to provide a real URL
	/// to put into the &lt;href&gt;-tag of the generated link, providing an
	/// standard link to search engines and a fallback for users with
	/// JavaScript turned off (though one with less functionality).
	/// <br/>
	/// IMPORTANT: This control does NOT work like a button, most noticeably
	/// it does not preserve the state of the page when JavaScript is turned
	/// off. No view state is persisted, if the real link is visited.
	/// </summary>
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:HyperLinkButton runat=server></{0}:HyperLinkButton>")]
	public class SeoLinkButton : LinkButton
	{
		/// <summary>
		/// The URL that will be navigated to if JavaScript is disabled
		/// (i.e. the Postback in the [onclick] handler is not executed).
		/// <br/>
		/// Serves search engines for navigation as well.
		/// </summary>
		[Bindable(true)]
		[Category("Behavior")]
		[DefaultValue("")]
		[Localizable(true)]
		public string FallbackUrl
		{
			get
			{
				String s = (String)ViewState["FallbackNavigateUrl"];
				return (s ?? String.Empty);
			}

			set
			{
				ViewState["FallbackNavigateUrl"] = value;
			}
		}
        
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			if (Page != null)
			{
				Page.VerifyRenderingInServerForm(this);
			}
			if (Enabled && !IsEnabled)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
			}
			string onClick = OnClientClick.EnsureEndsWith(";");
			if (HasAttributes)
			{
				string str2 = Attributes["onclick"];
				if (str2 != null)
				{
					onClick = onClick + str2.EnsureEndsWith(";");
					Attributes.Remove("onclick");
				}
			}
			if (IsEnabled && (Page != null))
			{
				PostBackOptions postBackOptions = GetPostBackOptions();
				string postBackEventReference = null;
				if (postBackOptions != null)
				{
					postBackEventReference = Page.ClientScript.GetPostBackEventReference(postBackOptions, true);
				}
				if (string.IsNullOrEmpty(postBackEventReference))
				{
					postBackEventReference = "void(0);";
				}
				onClick += postBackEventReference.EnsureEndsWith(";");
				onClick += "return false;";

				// Ensure that onclick starts with "javascript:" and has no occurrence in the middle.
				onClick = onClick.Replace("javascript:", "").EnsureStartsWith("javascript:");

				// Set the HRef here
				if (!string.IsNullOrEmpty(FallbackUrl))
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Href, FallbackUrl);
				}
			}
			if (onClick.Length > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Onclick, onClick);
			}

			base.AddAttributesToRender(writer);
		}
	}
}
