using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SpeakFriend.FileUploader;

namespace ImageUploader
{
    public partial class ExampleStoredWithSubmitButton : System.Web.UI.UserControl
    {
        private UploadManager _uploadManager = null;
        private readonly SessionUpload _sessionImageUpload = new SessionUpload();

        protected void Page_Load(object sender, EventArgs e)
        {
            btnSave.Click += btnSave_Click;
            _uploadManager = _sessionImageUpload.GetUploadManager(ucMultiUpload.UploadManagerId);
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            for (int key = 0; key < _uploadManager.ImagePaths.Count; key++)
            {
                if (!_uploadManager.ImagePaths.ContainsKey(key))
                    continue;

                var file = _uploadManager.ImagePaths[key];
                var fileInfo = new FileInfo(file);
                var fileName = fileInfo.Name.Split('.')[0];
                var content = System.Drawing.Image.FromFile(file);

                content.Save(
                    Path.Combine(UploadSettings.ImagesDirAbsolute, fileName + ".jpg"),
                    ImageFormat.Jpeg);
            }
        }
    }
}