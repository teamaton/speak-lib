using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;

namespace SpeakFriend.Utilities.Web
{
    public class HttpCurrentNoWeb : IHttpCurrent
    {
        IResponse IHttpCurrent.Response { get { return Response; } }
        IRequest IHttpCurrent.Request { get { return Request; } }
        IDictionary IHttpCurrent.ContextItems { get { return ContextItems; } }

        public RequestNoWeb Request { get; set; }
        public ResponseNoWeb Response { get; set; }
        public IDictionary ContextItems { get; set; }

        public void Reset()
        {
            Response = new ResponseNoWeb();
            Request = new RequestNoWeb();
        }
    }
}
