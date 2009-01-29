using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SpeakFriend.Utilities.Web
{
    public static class RenderDurationBuilder
    {
        public static RenderDuration GetEntry()
        {
            var result = new RenderDuration();

            result.RequestStart = DateTime.Now;

            if(ContextUtil.IsWebContext){
                result.RequestedPage = HttpContext.Current.Request.Path;
            }else{
                result.RequestedPage = "NonWebApplication_" + Guid.NewGuid();
            }

            return result;
        }
    }
}
