using System;

namespace SpeakFriend.Utilities
{
    [Serializable]
    public enum DistributionType
    {
        Internal = 1,
        Email = 2,
    }

    public class DistributionTypeHelper : EnumHelperBase<DistributionType>
    {
    }
}