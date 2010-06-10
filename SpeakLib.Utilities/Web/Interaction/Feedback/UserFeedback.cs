using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities.Web
{
    [Serializable]
    public class UserFeedback
    {
        public string Message { get; set; }
        public bool IsSet { get; set; }
        public bool Given { get; set; }
        
    }
}
