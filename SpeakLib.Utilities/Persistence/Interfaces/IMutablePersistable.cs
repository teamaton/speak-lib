using System;

namespace SpeakFriend.Utilities
{
    public interface IMutablePersistable : IPersistable
    {
        DateTime DateModified { get; set; }
    }
}