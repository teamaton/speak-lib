using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace SpeakFriend.Utils.Web
{
    public class SessBase
    {
        protected HttpSessionState Sess
        {
            get
            {
                return HttpContext.Current.Session;
            }
        }
        protected SessionData Data = new SessionData();
    }
}
