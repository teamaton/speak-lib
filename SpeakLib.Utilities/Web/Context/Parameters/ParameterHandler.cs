using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities.Web
{
    public class ParameterHandler
    {
        /// <summary>
        /// The name of the URL parameter to catch
        /// For eg: "domain.com?name=value" 
        /// </summary>
        public string Name;

        /// <summary>
        /// The description of what the Handler is supposed to, 
        /// do with the catched parameter.
        /// </summary>
        public string Description = "";

        public delegate void ActionDelegate(object value);
        public ActionDelegate Action;

        public bool ApliesOnlyLocal = false;
    }
}
