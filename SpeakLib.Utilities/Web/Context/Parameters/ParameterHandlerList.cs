using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities.Web
{
    public class ParameterHandlerList : List<ParameterHandler>
    {
        public bool Contains(object key)
        {
            if (key == null) return false;
            return GetByName(key.ToString()) != null;
        }

        public ParameterHandler GetByName(object key)
        {
            return Find(handler => handler.Name.ToLower() == key.ToString().ToLower());
        }
    }
}
