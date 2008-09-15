using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.TrueOrFalse.UserControls
{
    public partial class Login : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnLogin.Click += btnLogin_Click;
        }

        void btnLogin_Click(object sender, EventArgs e)
        {
            lblUserMessage.Text = "Der Login wurde noch nicht implementiert";
        }
    }
}