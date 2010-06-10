﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities.Web
{
    [Serializable]
    public enum ValidationType
    {
        RequiredField,
        Email,
        GUID,
        Compare,
        Uri,
        Positive_Integer,
        Price
    }
}
