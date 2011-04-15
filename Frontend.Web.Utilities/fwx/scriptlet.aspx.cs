using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SpeakFriend.Web.Utilities.fwx
{
	public partial class scriptlet : System.Web.UI.Page
	{
		private string _stage;

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			_stage = "OnInit";
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			_stage = "PageLoad";
		}

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			_stage = "OnPreRender";
		}

		protected override void Render(HtmlTextWriter writer)
		{
			_stage = "Render";
			base.Render(writer);
			_stage = "After Render";
		}

		protected string GetScriptletContent()
		{
			return _stage;
		}
	}
}