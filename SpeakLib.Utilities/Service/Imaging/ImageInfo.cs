using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities
{
    public class ImageInfo
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string AbsolutePath { get; set; }
        public string RelativePath { get; set; }
        public int HashCode { get; set; }
    }
}
