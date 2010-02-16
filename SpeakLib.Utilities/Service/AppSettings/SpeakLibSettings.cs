using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web.Configuration;

namespace SpeakFriend.Utilities
{
    public class SpeakLibSettings : AppSettings
    {
        #region Email

        public static bool EmailIsEnabled { get { return Get<bool>("EmailEnabled"); } }
        public static string EmailFrom { get { return WebConfigurationManager.AppSettings.Get("EmailFromAddress"); } }
        public static string EmailFromName { get { return WebConfigurationManager.AppSettings.Get("EmailFromName"); } }
        public static string EmailTo { get { return WebConfigurationManager.AppSettings.Get("EmailToAddress"); } }
        public static string EmailToName { get { return WebConfigurationManager.AppSettings.Get("EmailToName"); } }

        public static List<string> EmailSettings
        {
            get
            {
                return WebConfigurationManager.AppSettings.AllKeys.ToList().FindAll(
                    set => set.IndexOf("email", StringComparison.InvariantCultureIgnoreCase) >= 0 ||
                           set.IndexOf("smtp", StringComparison.InvariantCultureIgnoreCase) >= 0);
            }
        }

        public static string SmtpServer { get { return Get<string>("SmtpServer"); } }
        public static string SmtpPassword { get { return Get<string>("SmtpPassword"); } }
        public static string SmtpUser { get { return Get<string>("SmtpUser"); } }

        #endregion
    }
}
