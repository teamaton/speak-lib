using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace SpeakFriend.FileUpload
{
    internal class Settings
    {
        private static readonly AppSettingsReader _settingReader = new AppSettingsReader();

        public static string FileUploadTempDir { get { return Get<string>("FileUploadTempDir"); } }

        private static T Get<T>(string settingKey)
        {
            return (T)_settingReader.GetValue(settingKey, typeof(T));
        }
    }
}
