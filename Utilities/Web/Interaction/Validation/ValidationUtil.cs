using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SpeakFriend.Utilities.Web
{    
    public class ValidationUtil
    {
        public static bool IsEmail(string email)
        {
            if (String.IsNullOrEmpty(email))
                return false;

            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);

            if (re.IsMatch(email))
                return true;

            return false;
        }

        public static bool IsUri(string uri)
        {
            if (String.IsNullOrEmpty(uri))
                return false;

            string strRegex = @"^[a-z]+([a-z0-9-]*[a-z0-9]+)?(\.([a-z]+([a-z0-9-]*[a-z0-9]+)?)+)*$";
            Regex re = new Regex(strRegex);

            if (re.IsMatch(uri))
                return true;

            return false;
        }

    }
}
