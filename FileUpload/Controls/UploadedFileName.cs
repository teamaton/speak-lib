using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace SpeakFriend.FileUpload
{
    public class UploadedFileName : UploadedFileControl
    {
        private Label lblFileName;

        public UploadedFileName()
        {
            FileChanged += UploadedFileName_FileChanged;
        }

        void UploadedFileName_FileChanged(object sender, EventArgs e)
        {
            if(File == null) return;
            EnsureChildControls();
            lblFileName.Text = File.Name;
        }

        protected override void RecreateChildControls()
        {
            EnsureChildControls();
        }

        protected override void CreateChildControls()
        {
            Controls.Clear();

            if (lblFileName == null)
                lblFileName = new Label()
                                  {
                                      ID = "lblFileName"
                                  };

            Controls.Add(lblFileName);
        }
    }
}
