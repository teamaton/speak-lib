using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities.Web
{
    /// <summary>
    /// The time need to render a time
    /// </summary>
    public class RenderDuration
    {
        private DateTime _requestStart = DateTime.MinValue;
        private DateTime _requestEnd = DateTime.MinValue;

        public DateTime RequestStart { get { return _requestStart; } }
        public DateTime RequestEnd { get { return _requestEnd; } }
        
        public string RequestedPage = "";

        public TimeSpan Value{
            get{ return RequestEnd - RequestStart; }
        }

        public bool IsCompleted { get { return RequestEnd != DateTime.MinValue; } }

        public void StartsNow()
        {
            _requestStart = DateTime.Now;
        }

        public void StopsNow()
        {
            _requestEnd = DateTime.Now;
        }

    }
}
