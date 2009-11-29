using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SpeakFriend.Utilities.Web;

namespace Tests.Utilities.Web.Context
{
    [TestFixture]
    public class HttpContextTest
    {
        public void Usage()
        {
            var httpCurrent = HttpCurrent.GetNoWeb();
        }
    }
}
