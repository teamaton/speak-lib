using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities
{
    public interface IPersistable
    {
        int Id { get; set; }

        DateTime DateCreated { get; set; }
    }
}
