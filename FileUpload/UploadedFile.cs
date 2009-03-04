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
        public bool IsDeleted { get; private set; }

        public void Delete()
        {
            IsDeleted = true;
        }

        public UploadedFile()
        {
            TempKey = Guid.NewGuid();
        }

        public Guid TempKey { get; private set; }

        public string TempFileName
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
