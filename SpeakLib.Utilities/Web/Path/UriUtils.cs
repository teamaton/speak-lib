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
        /// www.speak-friend.com -> string.Empty.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static string FirstSubdomainNotWww(Uri uri)
        {
            var host = uri.Host;

            if (string.IsNullOrEmpty(host) || !host.Contains("."))
                return string.Empty;

            host = RemoveTopLevelDomain(host);

            // remove leading www.
            if (host.StartsWith("www."))
                host = host.Substring("www.".Length);

            var parts = host.Split('.');

            if (parts.Count() > 2)
                return parts[parts.Count()-3];

            return string.Empty;
        }

        public static string RemoveTopLevelDomain(string host)
        {
            foreach (var topLevelDomain in AllTopLevelDomains)
                if (host.EndsWith("." + topLevelDomain))
                    return host.Substring(0, host.Length - topLevelDomain.Length + 1);

            return host;
        }

        /// <summary>
        /// Returns all the subdomains in front of rootDomain as a string.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="rootDomain"></param>
        /// <returns></returns>
        public static string SubdomainString(Uri uri, string rootDomain)
        {
            var host = uri.Host;

            if (!uri.Host.EndsWith(rootDomain))
                throw new ArgumentException("The uri.Host [" + uri.Host + "] must end with the rootDomain [" + rootDomain + "]!",
                                            "rootDomain");
            
            var subdomains = host.Remove(host.IndexOf(rootDomain));

            if (subdomains.EndsWith(".")) // remove the dot
                subdomains = subdomains.Remove(subdomains.Length - 1);

            return subdomains;
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

        public static List<string> AllTopLevelDomains = new List<string> { "AC", "AD", "AE", "AERO", "AF", "AG", "AI", "AL", "AM", "AN", "AO", "AQ", "AR", "ARPA", "AS", "ASIA", "AT", "AU", "AW", "AX", "AZ", "BA", "BB", "BD", "BE", "BF", "BG", "BH", "BI", "BIZ", "BJ", "BM", "BN", "BO", "BR", "BS", "BT", "BV", "BW", "BY", "BZ", "CA", "CAT", "CC", "CD", "CF", "CG", "CH", "CI", "CK", "CL", "CM", "CN", "CO", "COM", "COOP", "CR", "CU", "CV", "CX", "CY", "CZ", "DE", "DJ", "DK", "DM", "DO", "DZ", "EC", "EDU", "EE", "EG", "ER", "ES", "ET", "EU", "FI", "FJ", "FK", "FM", "FO", "FR", "GA", "GB", "GD", "GE", "GF", "GG", "GH", "GI", "GL", "GM", "GN", "GOV", "GP", "GQ", "GR", "GS", "GT", "GU", "GW", "GY", "HK", "HM", "HN", "HR", "HT", "HU", "ID", "IE", "IL", "IM", "IN", "INFO", "INT", "IO", "IQ", "IR", "IS", "IT", "JE", "JM", "JO", "JOBS", "JP", "KE", "KG", "KH", "KI", "KM", "KN", "KP", "KR", "KW", "KY", "KZ", "LA", "LB", "LC", "LI", "LK", "LR", "LS", "LT", "LU", "LV", "LY", "MA", "MC", "MD", "ME", "MG", "MH", "MIL", "MK", "ML", "MM", "MN", "MO", "MOBI", "MP", "MQ", "MR", "MS", "MT", "MU", "MUSEUM", "MV", "MW", "MX", "MY", "MZ", "NA", "NAME", "NC", "NE", "NET", "NF", "NG", "NI", "NL", "NO", "NP", "NR", "NU", "NZ", "OM", "ORG", "PA", "PE", "PF", "PG", "PH", "PK", "PL", "PM", "PN", "PR", "PRO", "PS", "PT", "PW", "PY", "QA", "RE", "RO", "RS", "RU", "RW", "SA", "SB", "SC", "SD", "SE", "SG", "SH", "SI", "SJ", "SK", "SL", "SM", "SN", "SO", "SR", "ST", "SU", "SV", "SY", "SZ", "TC", "TD", "TEL", "TF", "TG", "TH", "TJ", "TK", "TL", "TM", "TN", "TO", "TP", "TR", "TRAVEL", "TT", "TV", "TW", "TZ", "UA", "UG", "UK", "US", "UY", "UZ", "VA", "VC", "VE", "VG", "VI", "VN", "VU", "WF", "WS", "YE", "YT", "YU", "ZA", "ZM", "ZW" };
    }
}