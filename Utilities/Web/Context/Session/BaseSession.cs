using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace SpeakFriend.Utilities.Web
{
    public class BaseSession
    {
        private static HttpSessionState SessionState{ get { return HttpContext.Current.Session; } }
        protected SessionData Data = new SessionData();

        public void Kill()
        {
            SessionState.Abandon();
            HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri);
        }

        public bool IsSessionExpired()
        {
            if (SessionState != null)
            {
                if (SessionState.IsNewSession)
                {
                    string cookieHeader = HttpContext.Current.Request.Headers["Cookie"];
                    if ((null != cookieHeader) && (cookieHeader.IndexOf("ASP.NET_SessionId") >= 0))
                    {
                        //if (Request.IsAuthenticated == true)
                        //{
                        //    //Do something magical here!
                        //}
                    }
                }
            }
            return true;
        }
    }
}
