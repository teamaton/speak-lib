using System;

namespace SpeakFriend.Utilities
{
    public interface IMutableDomainObject : IDomainObject
    {
        DateTime Modified { get; set; }
    }
}