
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace SpeakFriend.FileUploader
{
    public class UploadSettings
    {
        private static readonly AppSettingsReader _settingReader = new AppSettingsReader();

        public static string ImagesDirAbsolute
        {
            get { return (string)_settingReader.GetValue("SF.UploadDirAbsolute", typeof(string)); }
        }

        public static string ImagesDirRelative
        {
            get { return (string)_settingReader.GetValue("SF.UploadDirRelative", typeof(string)); }
        }

        public static int DebugDelayInSeconds
        {
            get { return (int)_settingReader.GetValue("SF.UploadDebugDelayInSeconds", typeof(int));  }
        }

        private static readonly Configuration config = WebConfigurationManager.OpenWebConfiguration("~");
        private static readonly HttpRuntimeSection section = config.GetSection("system.web/httpRuntime") as HttpRuntimeSection;
        
        public static double MaxUploadSizeInMB
        {
            get { return (double)Math.Round(section.MaxRequestLength / 1024.0, 1); }
        }

    }
}
