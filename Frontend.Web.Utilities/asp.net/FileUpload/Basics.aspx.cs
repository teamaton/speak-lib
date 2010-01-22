using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SpeakFriend.Web.Utilities
{
    public partial class FileUploadExample : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnSubmit.Click += btnSubmit_Click;
        }

        void btnSubmit_Click(object sender, EventArgs e)
        {
            if(FileUploadFrame1.UploadedFiles.Count==0)
                mvUploadFileDemo.SetActiveView(vwNoFile);
            else
            {
                var file = FileUploadFrame1.UploadedFiles.Last();
                lblFileName.Text = file.Name;
                lblTempPath.Text = file.TempFilePathAbsolute;
                mvUploadFileDemo.SetActiveView(vwSubmitted);
            }
        }
    }
}
