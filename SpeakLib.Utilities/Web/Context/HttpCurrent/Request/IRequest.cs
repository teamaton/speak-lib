﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SpeakFriend.Utilities.Web
{
    public interface IRequest
    {
        Uri Url { get; }
        string[] UserLanguages { get;  }
        HttpCookieCollection Cookies { get; }
        

    }
}
