using System;

namespace SpeakFriend.Utilities
{
    public static class EnumExtensions
    {
        public static int ToInt(this Enum en)
        {
            return Convert.ToInt32(en);
        }

        public static string ToIntString(this Enum en)
        {
            return Convert.ToInt32(en).ToString();
        }
    }
}
