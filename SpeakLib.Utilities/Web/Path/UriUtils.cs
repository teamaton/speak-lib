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
        /// <param name="rootDomain"></param>
        /// <returns></returns>
        public static string FirstSubdomainNotWww(Uri uri, string rootDomain)
        {
            var host = uri.Host;

            if (string.IsNullOrEmpty(host))
                return string.Empty;

            // remove leading www.
            if (host.StartsWith("www."))
                host = host.Substring("www.".Length);

            var parts = host.Split('.');
            var rootParts = rootDomain.Split('.');

            // no subdomain
            if (parts[0].Equals(rootParts[0]))
                return string.Empty;

            return parts[0];
        }

        /// <summary>
        /// Returns a new URL string with subdomain.domain as the host part. Ignores/throws away "www".
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="rootDomain"></param>
        /// <param name="newSubdomain"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">If <paramref name="rootDomain"/> is not the last part of the host of the given <paramref name="uri"/>.</exception>
        public static string ChangeSubdomain(Uri uri, string rootDomain, string newSubdomain)
        {
            if (!uri.Host.EndsWith(rootDomain))
                throw new ArgumentException("The uri.Host [" + uri.Host + "] must end with the rootDomain [" + rootDomain + "]!",
                                            "rootDomain");

            // ignore "www"
            var domain = (newSubdomain + "." + rootDomain).ToLowerInvariant();

            return uri.Scheme + "://" + domain + uri.PathAndQuery;
        }
    }
}
