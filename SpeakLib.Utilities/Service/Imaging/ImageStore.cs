using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities
{
    public class ImageStore
    {
        private readonly string _pathAbsolute;
        private readonly string _pathRelative;

        public ImageStore(string pathAbsolute, string pathRelative)
        {
            _pathAbsolute = pathAbsolute;
            _pathRelative = pathRelative;
        }

        public void Store(string key, string sourcePath)
        {
            var image = Image.FromFile(sourcePath);
            image.Save(GetPathAbsolute(key), ImageFormat.Png);
        }
  
        public string Get(string key)
        {
            //ToDo: check if exists and if not, return default placeholder
            return GetPathRelative(key);
        }

        private string GetPathAbsolute(string key)
        {
            return Path.Combine(_pathAbsolute, string.Format("{0}.png", key));
        }

        private string GetPathRelative(string key)
        {
            return Path.Combine(_pathRelative, string.Format("{0}.png", key));
        }
    }
}
