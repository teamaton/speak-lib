using System;
using System.Collections;
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
        IDictionary ContextItems { get; }
		string SessionId { get; }
    }

    public class HttpCurrent : IHttpCurrent
    {
        public virtual IRequest Request { get; protected set; }
        public virtual IResponse Response { get; protected set; }
        public virtual IDictionary ContextItems { get; protected set; }
		public virtual string SessionId { get { return HttpContext.Current.Session.SessionID; } }

        protected HttpCurrent()
        {
        	Request = new RequestWeb();
        	Response = new ResponseWeb();
        	ContextItems = HttpContext.Current.Items;
        }

        public static HttpCurrent Get()
        {
        	return new HttpCurrent();
        }

        public static HttpCurrentNoWeb GetNoWeb()
        {
        	return new HttpCurrentNoWeb();
        }
    }
}
