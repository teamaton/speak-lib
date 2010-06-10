using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities
{
    [Serializable]
    public class EnumAnnotatedList : List<EnumAnnotated> 
    {
        public EnumAnnotated GetById(int id)
        {
            foreach (var enumAnnotated in this)
                if (enumAnnotated.Value == id)
                    return enumAnnotated;

            return null;
        }
    }
}
