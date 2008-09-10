using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.TrueOrFalse
{
    public class SourceService
    {
        public Source GetInteressanteFaktenDe()
        {
            return new Source
                       {
                           Name = "Interessante-Fakten.de",
                           URL = "http://interessantefakten.de/"
                       };
        }
    }
}
