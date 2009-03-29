using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SpeakFriend.Utilities;
using SpeakFriend.Utilities.Web;

namespace SpeakFriend.Web.Utilities
{
    public partial class frmPager : Page
    {
        private IPager _pager;

        protected void Page_Load(object sender, EventArgs e)
        {
            _pager = new SessionData()
                .Get("sf_Example_Pager",
                     () => new Pager {TotalItems = 123, PageSize = 10});
            
            PagerControl1.Register(_pager);
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            ltPage.Text = _pager.CurrentPage.ToString();
        }
    }
}
