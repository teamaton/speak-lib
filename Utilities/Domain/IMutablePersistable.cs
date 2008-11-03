using System;

namespace SpeakFriend.Utilities
{
    public interface IMutablePersistable : IPersistable
    {
        DateTime Modified { get; set; }
    }
}