using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities.Web
{
    public class HttpCurrentNoWeb : IHttpCurrent
    {
        IResponse IHttpCurrent.Response { get { return Response; } }
        IRequest IHttpCurrent.Request { get { return Request; } }

        public RequestNoWeb Request { get; set; }
        public ResponseNoWeb Response { get; set; }
    }
}
