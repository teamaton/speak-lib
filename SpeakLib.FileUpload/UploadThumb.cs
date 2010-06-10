using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SpeakFriend.FileUpload
{
    [Serializable]
    public class UploadThumb
    {
        public UploadThumb()
        {
            TempKey = Guid.NewGuid();
        }

        public Guid TempKey { get; private set; }

        public string ThumbPathAbsolute
        {
            get
            {
                return Path.Combine(
                    Path.Combine(Settings.FileUploadTempDirAbsolute, "thumbs"),
                    string.Format("thumb-{0}.png", TempKey));
            }
        }

        public string ThumbPathRelative
        {
            get
            {
                return Path.Combine(
                    Path.Combine(Settings.FileUploadTempDirRelative, "thumbs"),
                    string.Format("thumb-{0}.png", TempKey));
            }
        }
    }
}
