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
    [ToolboxData("<{0}:Pager runat=server></{0}:Pager>")]
    public class PagerControl : CompositeControl
    {
        private IPager _pager;

        public void Register(IPager pager)
        {
            _pager = pager;
            CreateChildControls();
        }

        protected override void RecreateChildControls()
        {
            EnsureChildControls();
        }

        protected override void CreateChildControls()
        {
            Controls.Clear();

            if (_pager == null) return;


        }
    }
}
