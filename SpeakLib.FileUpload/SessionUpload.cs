using System;
using System.Collections.Generic;
using SpeakFriend.Utilities.Web;


namespace SpeakFriend.FileUpload
{
    public class SessionUpload : BaseSession
    {
        private readonly string uploadManagerId;

        public SessionUpload(string id)
        {
            uploadManagerId = id;
        }

        public UploadManager UploadManager
        {
            get
            {
                var keyName = GetKeyName(uploadManagerId);
                if (Data[keyName] == null)
					lock(keyName)
						if (Data[keyName] == null)
							UploadManager = new UploadManager();

                return (UploadManager) Data[keyName];
            }
            set
            {
                var keyName = GetKeyName(uploadManagerId);
                if (Data[keyName] != null)
                    ((UploadManager) Data[keyName]).Dispose();

                Data[keyName] = value;
            }
        }

        private static string GetKeyName(string id)
        {
            return string.Format("SF.UploadManager.{0}", id);
        }
    }
}
