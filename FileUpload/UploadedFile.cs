using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SpeakFriend.FileUpload
{
    [Serializable]
    public class UploadedFile
    {
        public string Name { get; set; }
        
        public UploadedFile()
        {
            TempKey = Guid.NewGuid();
        }

        public Guid TempKey { get; private set; }

        public string TempFilePath
        {
            get
            {
                return Path.Combine(
                    Settings.FileUploadTempDir,
                    string.Format("{0}.tmp", TempKey));
            }
        }
    }
}
