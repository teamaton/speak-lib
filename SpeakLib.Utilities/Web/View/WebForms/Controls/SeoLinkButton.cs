using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SpeakFriend.Utilities
{
	/// <summary>
	/// This extended LinkButton offers the possibility to provide a real URL
	/// to put into the &lt;href&gt;-tag of the generated link, providing a
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
				var s = (string) ViewState["FallbackNavigateUrl"];
				return (s ?? string.Empty);
			}

			set { ViewState["FallbackNavigateUrl"] = value; }
		}

		/// <summary>
		/// Whether to suppress rendering of the postback javascript snippet that is normally rendered for LinkButtons.
		/// </summary>
		public bool SuppressDefaultPostback { get; set; }

		/// <remarks>
		/// FallbackUrl | OnClientClick | Result
		/// ------------+---------------+---------------------------------------------------------------------------------------------------------
		/// ---         | ---           | <a href="javascript:__doPostBack('id','')"></a> - same as LinkButton
		/// ---         | alert('me!')  | <a href="javascript:__doPostBack('id','')" onclick="javascript:alert('me!')"></a> - same as LinkButton
		/// http://g.de | ---           | <a href="http://g.de" onclick="javascript:__doPostBack('id','');return false;"></a> - add "return false;"
		/// http://g.de | alert('me!')  | <a href="http://g.de" onclick="javascript:alert('me!');__doPostBack('id','');return false;"></a>
		/// http://g.de | alert('me!')  | SuppressDefaultPostback=true: <a href="http://g.de" onclick="javascript:alert('me!')"></a>
		/// ---         | ---           | SuppressDefaultPostback=true: <a href="javascript:void(0)"></a>
		/// </remarks>
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			if (Page != null)
			{
				Page.VerifyRenderingInServerForm(this);
			}

			// IsEnabled returns the effective Enabled value based on all parents' Enabled value
			var isEffectivelyEnabled = base.IsEnabled;

			if (IsRenderDisabledAttribute(isEffectivelyEnabled))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
			}

			var mergedOnClick = GetMergedOnClick();
			var postBackEventReference = GetPostBackEventReference(isEffectivelyEnabled);

			if (!string.IsNullOrWhiteSpace(FallbackUrl))
			{
				// special behavior with FallbackUrl
				writer.AddAttribute(HtmlTextWriterAttribute.Href, FallbackUrl);

				// append postBack code to onclick attribute
				var onclickEffective = mergedOnClick;

				if (!SuppressDefaultPostback)
				{
					// add "return false;" at end of postback code when rendered in onclick handler,
					// so that the FallbackUrl will not be opened
					onclickEffective =
						onclickEffective.EnsureEndsWith(";", StringEnsureOptions.IgnoreNullOrEmpty)
						+ postBackEventReference
						  	.EnsureStartsNotWith("javascript:")
						  	.EnsureEndsWith(";", StringEnsureOptions.IgnoreNullOrEmpty)
						  	.EnsureEndsWith("return false;", StringEnsureOptions.IgnoreNullOrEmpty);
				}

				if (!string.IsNullOrEmpty(onclickEffective))
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Onclick, onclickEffective);
				}
			}
			else
			{
				// default behavior as in LinkButton
				if (!string.IsNullOrEmpty(mergedOnClick))
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Onclick, mergedOnClick);
				}
				if (SuppressDefaultPostback)
				{
					// would love to not render the href attribute at all: http://stackoverflow.com/q/134845/177710
					// but need to render *some* value, because else it will be rendered by the base class, i.e.
					// LinkButton. use a no-op javascript instruction instead of # because I don't like that.
					writer.AddAttribute(HtmlTextWriterAttribute.Href, "javascript:void(0)");
				}
				else if (isEffectivelyEnabled && !string.IsNullOrEmpty(postBackEventReference))
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Href, postBackEventReference);
				}
			}

			// only call the base method here, after rendering our custom onclick and href attributes
			// otherwise, the LinkButton code would first render those attributes and ignore FallbackUrl
			base.AddAttributesToRender(writer);
		}

		private string GetPostBackEventReference(bool isEffectivelyEnabled)
		{
			string postBackEventReference = null;
			if (isEffectivelyEnabled && (Page != null))
			{
				postBackEventReference = Page.ClientScript.GetPostBackEventReference(GetPostBackOptions(), true);

				// this is the default behavior copied from LinkButton
				if (string.IsNullOrEmpty(postBackEventReference))
				{
					postBackEventReference = "javascript:void(0)";
				}
			}
			return postBackEventReference;
		}

		private bool IsRenderDisabledAttribute(bool isEffectivelyEnabled)
		{
			// Enabled refers only to the current control
			return (Enabled && !isEffectivelyEnabled) && SupportsDisabledAttribute;
		}

		/// <summary>
		/// Returns a string with the contents of OnClientClick and [onclick]. Maybe empty. Never NULL.
		/// </summary>
		private string GetMergedOnClick()
		{
			var onclientclick = OnClientClick.EnsureEndsWith(";", StringEnsureOptions.IgnoreNullOrEmpty);
			var mergedOnClick = onclientclick;
			if (base.HasAttributes)
			{
				var onclick = base.Attributes["onclick"];
				if (onclick != null)
				{
					mergedOnClick += onclick;
					base.Attributes.Remove("onclick");
				}
			}
			return mergedOnClick;
		}
	}
}