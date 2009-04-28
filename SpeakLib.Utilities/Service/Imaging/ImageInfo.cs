using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities
{
    public class ImageInfo
    {
        public string Name { get; set; }
        public string GroupKey { get; set; }
        public int Id { get; set; }
        public string AbsolutePath { get; set; }
        public string RelativePath { get; set; }
        public int HashCode { get; set; }
        public bool UseJpeg { get; set; }

        public long FileSize { get { return new FileInfo(AbsolutePath).Length; } }
        public string FileExtension { get { return Path.GetExtension(AbsolutePath); } }
        public DateTime CreationTime { get { return File.GetCreationTime(AbsolutePath); } }

    }
}
