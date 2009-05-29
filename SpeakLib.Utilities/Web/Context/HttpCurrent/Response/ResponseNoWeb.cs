﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SpeakFriend.Utilities.Web
{
    public class ResponseNoWeb : Response, IResponse
    {
        public void Redirect(string url)
        {
            _redirections.Add(url);
        }

        public void Redirect(string url, bool endResponse)
        {
            _redirections.Add(url);
        }

        public void SetCookie(HttpCookie cookie)
        {
            _cookiesCreated.Add(cookie);
        }
    }
}