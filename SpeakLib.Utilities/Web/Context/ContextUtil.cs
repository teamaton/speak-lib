using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SpeakFriend.Utilities.Web
{
    public static class ContextUtil
    {
        public static bool IsLocal
        {
            get
            {
                if (!IsWebContext) // helps unit testing
                    return false;

                return HttpContext.Current.Request.IsLocal;
            }
        }

        public static bool IsWebContext
        {
            get { return HttpContext.Current != null; }
        }
    }
}
