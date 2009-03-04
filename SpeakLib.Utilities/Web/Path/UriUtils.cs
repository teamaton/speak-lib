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
		
        /// <summary>
        /// www.pl.speak-friend.com -> pl
        /// www.speak-friend.com -> speak-friend
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static string FirstSubdomainNotWww(Uri uri)
        {
            var host = uri.Host;

            if (string.IsNullOrEmpty(host))
                return string.Empty;

            var parts = host.Split('.');
            if ("www".Equals(parts[0], StringComparison.InvariantCultureIgnoreCase))
                return parts[1];

            return parts[0];
        }
    }
}
