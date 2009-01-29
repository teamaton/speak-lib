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
        public DateTime RequestStart = DateTime.MinValue;
        public DateTime RequestEnd = DateTime.MinValue;
        public string RequestedPage = "";

        public TimeSpan Value{
            get{ return RequestEnd - RequestStart; }
        }

        public bool IsCompleted { get { return RequestEnd != DateTime.MinValue; } }
    }
}
