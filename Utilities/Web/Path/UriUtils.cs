using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities.Web
{
    public class UriUtils
    {
        /// <summary>
        /// Gets the filename + extension of a given path, 
        /// </summary>
        /// <remarks>
        /// The difference to: System.IO.Path.GetFileName(filePath) is the removal of the query string.
        /// </remarks>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetFileName(string filePath)
        {
            // {frk} Warum nicht so?
            // return System.IO.Path.GetFileName(filePath);

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
		
        public static string GetQueryString(string requestPath)
        {
            if (!requestPath.Contains('?'))
                return "";

            return requestPath.Split('?')[1];
        }

        public static bool IsRelativePath(string value)
        {
            return !string.IsNullOrEmpty(value) && value.StartsWith("/");
        }
		
    }
}
