using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SpeakFriend.Utilities.Web
{    
    public class ValidationUtil
    {
        public const string Regex_Email =
            @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

        public const string Regex_Uri = 
            @"^[a-z]+([a-z0-9-]*[a-z0-9]+)?(\.([a-z]+([a-z0-9-]*[a-z0-9]+)?)+)*$";

        public const string Regex_GUID =
            @"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$";

        public static bool IsEmail(string email)
        {
            if (String.IsNullOrEmpty(email))
                return false;

            var re = new Regex(Regex_Email);

            if (re.IsMatch(email))
                return true;

            return false;
        }

        public static bool IsUri(string uri)
        {
            if (String.IsNullOrEmpty(uri))
                return false;
            
            var regex = new Regex(Regex_Uri);

            if (regex.IsMatch(uri))
                return true;

            return false;
        }

        public static bool IsGuid(string guid)
        {
            if (String.IsNullOrEmpty(guid))
                return false;

            var regex = new Regex(Regex_GUID);

            if (regex.IsMatch(guid))
                return true;

            return false;
        }

    }
}
