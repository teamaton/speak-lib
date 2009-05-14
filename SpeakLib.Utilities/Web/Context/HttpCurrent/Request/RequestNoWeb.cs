using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SpeakFriend.Utilities.Web
{
    public class RequestNoWeb : IRequest
    {
        public Uri Url { get; set; }
        public string[] UserLanguages { get; set; }
        public HttpCookieCollection Cookies { get; set; }

        public RequestNoWeb()
        {
            UserLanguages = new string[]{};
            Cookies = new HttpCookieCollection();
        }

    }
}
