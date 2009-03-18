using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace SpeakFriend.FileUpload
{
    public abstract class UploadedFileControl : CompositeControl
    {
        public UploadManager UploadManager { get; set; }

        protected event EventHandler FileChanged;
        private UploadedFile file;
        public UploadedFile File
        {
            get { return file; }
            set
            {
                file = value;
                if (FileChanged != null)
                    FileChanged(this, new EventArgs());
            }
        }
    }
}
