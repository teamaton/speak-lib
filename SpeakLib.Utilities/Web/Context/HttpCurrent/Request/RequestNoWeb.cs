using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
        public NameValueCollection QueryString { get { return UriUtils.GetNameValueCollectionFromQuery(Url.Query); } }
    	public string Path
    	{
			get { return Url.PathAndQuery.Split('?')[0]; }
    	}

    	public RequestNoWeb()
        {
            UserLanguages = new string[]{};
            Cookies = new HttpCookieCollection();
        }


        public void Clear()
        {
            
        }
    }
}
