using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utils
{
    public class PathUtils
    {
        /// <summary>
        /// Gets the filename + extension of a given path
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetFileName(string filePath)
        {
            string[] fileParts = filePath.Split('/');
            string fileName = fileParts[fileParts.Length - 1];

            fileName = RemoveQueryString(fileName);

            return fileName;
        }

        /// <summary>
        /// Removes a web query string, from a given path.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string RemoveQueryString(string fileName)
        {
            int indexOfQuestion = fileName.IndexOf("?");

            if (indexOfQuestion != -1)
                fileName = fileName.Remove(fileName.IndexOf("?"));
            return fileName;
        }
    }
}
