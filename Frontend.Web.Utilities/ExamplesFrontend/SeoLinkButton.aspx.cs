using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SpeakFriend.Web.Utilities
{
    public partial class frmSeoLinkButton : System.Web.UI.Page
    {
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				FillSeoLinkButton();

				HandleParam();
			}
		}

    	private void HandleParam()
    	{
    		var show = Context.Request.QueryString["show"];

			if (string.IsNullOrEmpty(show))
				return;

    		var showBool = Boolean.Parse(show);

			if (showBool)
				OnClick(this, EventArgs.Empty);
    	}

    	private void FillSeoLinkButton()
    	{
    		SeoLinkButton1.FallbackUrl = Context.Request.Path + "?show=true";
    	}

    	protected void OnClick(object sender, EventArgs e)
    	{
    		lblHidden.Visible = true;
    	}

    	protected void btnHide_OnClick(object sender, EventArgs e)
    	{
    		lblHidden.Visible = false;
    	}
    }
}
