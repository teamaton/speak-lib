using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace SpeakFriend.FileUpload
{
    public class UploadedFileImage : UploadedFileControl
    {
        private Image imgFileImage;

        public UploadedFileImage()
        {
            FileChanged += UploadedFileName_FileChanged;
        }

        public int ThumbSize { get; set; }

        void UploadedFileName_FileChanged(object sender, EventArgs e)
        {
            if(File == null) return;

            EnsureChildControls();

            imgFileImage.AlternateText = File.Name;

            if (ThumbSize == 0) 
                imgFileImage.ImageUrl = File.TempFilePathRelative;
            else
            {
                var thumb = UploadManager.ThumbGenerator.GetThumb(File, ThumbSize);
                if (thumb != null) imgFileImage.ImageUrl = thumb.ThumbPathRelative;
            }
        }

        protected override void RecreateChildControls()
        {
            EnsureChildControls();
        }

        protected override void CreateChildControls()
        {
            Controls.Clear();

            if (imgFileImage == null)
                imgFileImage = new Image
                                  {
                                      ID = "imgFileImage"
                                  };

            Controls.Add(imgFileImage);
        }
    }
}
