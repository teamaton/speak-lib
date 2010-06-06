using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;

namespace SpeakFriend.Utilities.Web
{
    public interface IHttpCurrent
    {
        IRequest Request { get; }
        IResponse Response { get; }
        HttpContext Context { get; }
    }

    public class HttpCurrent : IHttpCurrent
    {
        public virtual IRequest Request { get; protected set; }
        public virtual IResponse Response { get; protected set; }
        public virtual HttpContext Context { get; protected set; }

        protected HttpCurrent(){}

        public static HttpCurrent Get()
        {
            var result = new HttpCurrent
                             {
                                 Request = new RequestWeb(),
                                 Response = new ResponseWeb(),
                                 Context = HttpContext.Current
                             };

            return result;
        }

        public static HttpCurrentNoWeb GetNoWeb()
        {
            var result = new HttpCurrentNoWeb
                             {
                                 Request = new RequestNoWeb(),
                                 Response = new ResponseNoWeb()
                             };

            return result;
        }
    }
}
