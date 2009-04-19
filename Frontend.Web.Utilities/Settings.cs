using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SpeakFriend.Web.Utilities
{
    internal class Settings
    {
        private static readonly AppSettingsReader _settingReader = new AppSettingsReader();

        public static string FileManagerExampleDirRelative { get { return Get<string>("FileManagerExampleDirRelative"); } }
        public static string FileManagerExampleDirAbsolute { get { return Get<string>("FileManagerExampleDirAbsolute"); } }

        private static T Get<T>(string settingKey)
        {
            return (T)_settingReader.GetValue(settingKey, typeof(T));
        }
    }
}
