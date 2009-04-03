using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SpeakFriend.Utilities
{
    [ToolboxData("<{0}:HoverImage runat=server></{0}:HoverImage>")]
    public class HoverImage : HtmlImage
    {
        public HoverImage()
        {
            HoverSuffix = "-on";
        }

        [Bindable(false), Category("Appearance"), DefaultValue("-on"), Localizable(false)]
        public string HoverSuffix { get; set; }

        protected override void Render(HtmlTextWriter writer)
        {
            var hoverSrc = Src.Insert(Src.IndexOf('.'), HoverSuffix);

            Attributes.Add("onmouseover", string.Format("src='{0}'", hoverSrc));
            Attributes.Add("onmouseout", string.Format("src='{0}'", Src));
            
            base.Render(writer);
        }
    }
}
