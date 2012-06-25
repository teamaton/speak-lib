using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SpeakFriend.Utilities
{
	[ToolboxData("<{0}:HoverImage runat=server></{0}:HoverImage>")]
	public class HoverImageButton : ImageButton
	{
		public HoverImageButton()
		{
			HoverSuffix = "-on";
		}

		[Bindable(false), Category("Appearance"), DefaultValue("-on"), Localizable(false)]
		public string HoverSuffix { get; set; }

		protected override void Render(HtmlTextWriter writer)
		{
			var hoverSrc = ImageUrl.Insert(ImageUrl.LastIndexOf('.'), HoverSuffix);

			Attributes.Add("onmouseover", string.Format("src='{0}'", hoverSrc));
			Attributes.Add("onmouseout", string.Format("src='{0}'", ImageUrl));

			base.Render(writer);
		}
	}
}