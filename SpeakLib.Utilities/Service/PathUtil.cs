using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities
{
    public static class PathUtil
    {
        public static string GetRelativePath(string path, string rootDirectory)
        {
            return path.Replace(rootDirectory + Path.DirectorySeparatorChar, "");
        }
    }
}
