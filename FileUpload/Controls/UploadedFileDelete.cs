using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using SpeakFriend.Utilities.Web;

namespace SpeakFriend.FileUpload
{
    public class UploadedFileDelete : UploadedFileControl
    {

        private LinkButton btnDelete;

        protected override void OnLoad(EventArgs e)
        {
            EnsureChildControls();
            if (btnDelete != null)
                btnDelete.Click += btnDelete_Click;

        }
        
        void btnDelete_Click(object sender, EventArgs e)
        {
            UploadManager.RemoveFile(File.TempKey);
        }

        protected override void RecreateChildControls()
        {
            EnsureChildControls();
        }

        protected override void CreateChildControls()
        {
            Controls.Clear();

            if (btnDelete == null)
                btnDelete = new LinkButton
                                {
                                    ID = "btnDelete",
                                    Text = "[Delete]"
                                };

            Controls.Add(btnDelete);
        }
    }
}
