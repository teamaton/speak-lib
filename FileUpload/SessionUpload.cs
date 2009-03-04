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
                var name = string.Format("SF.UploadManager.{0}", uploadManagerId);
                if (Data[name] == null)
                    UploadManager = new UploadManager();

                return (UploadManager) Data[name];
            }
            set
            {
                var name = string.Format("SF.UploadManager.{0}", uploadManagerId);
                if (Data[name] != null)
                    ((UploadManager) Data[name]).Dispose();

                Data[name] = value;
            }
        }
    }
}
