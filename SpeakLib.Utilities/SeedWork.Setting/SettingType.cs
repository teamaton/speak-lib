using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities
{
    /// <summary>
    /// Convenience class for generating strings used as <see cref="SettingType"/>.
    /// </summary>
    [Serializable]
    public static class SettingType
    {
        /// <summary>
        /// Always returns the same string for a given object/type to use as 
        /// SettingType when going to the DB.
        /// </summary>
        public static string From(object obj)
        {
            return From(obj.GetType());
        }

        /// <summary>
        /// Always returns the same string for a given object/type to use as 
        /// SettingType when going to the DB.
        /// </summary>
        public static string From(Type type)
        {
            return From(type.Name);
        }

        /// <summary>
        /// Always returns the same string for a given object/type/string to use as 
        /// SettingType when going to the DB.
        /// </summary>
        public static string From(string settingTypeString)
        {
            return settingTypeString;
        }
    }
}
