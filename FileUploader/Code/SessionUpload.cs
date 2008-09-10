using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using SpeakFriend.Utils.Web;

namespace SpeakFriend.FileUploader
{
    public class SessionUpload : SessBase
    {
        /// <summary>
        /// Keeping references in order to dispose the UploadManagers
        /// correctly even when Sess has already been removed
        /// </summary>
        private Dictionary<string, UploadManager> _uploadManagers = new Dictionary<string, UploadManager>();

        public UploadManager GetUploadManager(string id)
        {
            var name = string.Format("SF.UploadManager.{0}", id);
            if (Data[name] == null)
                SetUploadManager(new UploadManager(), id);

            return (UploadManager)Data[name];
        }
        public void SetUploadManager(UploadManager value, string id)
        {
            var name = string.Format("SF.UploadManager.{0}", id);
            if (Data[name] != null)
                ((UploadManager)Data[name]).Dispose();

            Data[name] = value;
            if (value != null)
                _uploadManagers.Add(id, value);
            else
                _uploadManagers.Remove(id);
        }
        
        public void DisposeUploadManager(string id)
        {
            SetUploadManager(null, id);
        }

        ~SessionUpload()
        {
            foreach (var uploadManager in _uploadManagers.Values)
                if (uploadManager != null) uploadManager.Dispose();
        }
    }
}
