using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SpeakFriend.Utilities;

namespace SpeakFriend.Web.Utilities
{
    public partial class frmFileManager : System.Web.UI.Page
    {
        private ImageStore _imageStore = new ImageStore(Settings.FileManagerExampleDirAbsolute,
                                                        Settings.FileManagerExampleDirRelative);

        protected void Page_Load(object sender, EventArgs e)
        {
            FileManager1.Register(_imageStore, "examples");
        }
    }
}
