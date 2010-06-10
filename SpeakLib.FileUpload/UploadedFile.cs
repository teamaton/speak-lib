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

        public string TempFilePathAbsolute
        {
            get
            {
                return Path.Combine(
                    Settings.FileUploadTempDirAbsolute,
                    string.Format("tmp-{0}{1}", TempKey, Path.GetExtension(Name)));
            }
        }

        public string TempFilePathRelative
        {
            get
            {
                return Path.Combine(
                    Settings.FileUploadTempDirRelative,
                    string.Format("tmp-{0}{1}", TempKey, Path.GetExtension(Name)));
            }
        }
    }
}
