using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpeakFriend.Utilities;

namespace SpeakFriend.Utilities
{
    public class EmailAddresses
    {
        /// <summary>
        /// Ready-to-use MessageAddress for camping.info Admin.
        /// </summary>
        public static readonly MessageAddress AdminMA = new MessageAddress(Admin, "Admin");
        public static readonly string Admin = "admin@camping.info";

        /// <summary>
        /// Ready-to-use MessageAddress for camping.info System.
        /// </summary>
        public static readonly MessageAddress SystemMA = new MessageAddress(Admin, "camping.info");
        public static readonly string System = "system@camping.info";
    }
}
