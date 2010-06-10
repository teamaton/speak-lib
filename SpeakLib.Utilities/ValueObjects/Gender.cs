﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities
{
    [Serializable]
    public enum Gender
    {
        Unspecified,
        Female,
        Male        
    }

    public class GenderHelper : EnumHelperBase<Gender>
    {

    }
}
