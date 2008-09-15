using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpeakFriend.Utils.Web;

namespace SpeakFriend.TrueOrFalse
{
    public class UserSession : BaseSession
    {
        public void Clear()
        {
            IsLoggedIn = false;
        }

        public bool IsLoggedIn
        {
            get { return Data.Get<bool>("uc_IsLoggedIn", false); }
            set { Data["uc_IsLoggedIn"] = value; }
        }

        public void Logout()
        {
            IsLoggedIn = false;
        }
    }
}
