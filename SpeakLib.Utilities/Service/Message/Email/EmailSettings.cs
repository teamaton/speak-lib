using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpeakFriend.Utilities;

namespace SpeakFriend.Utilities
{
    [Serializable]
    public class EmailSettings : EntitySettingList
    {
        public static string Type = SettingTypes.Email;
        public static int TypeId = 1;

        // Settings

        public SettingBoolean EmailEnabled { get { return Get<SettingBoolean>("EmailEnabled"); } }
        public SettingString EmailFromAddress { get { return Get<SettingString>("EmailFromAddress"); } }
        public SettingString EmailFromName { get { return Get<SettingString>("EmailFromName"); } }
        public SettingString EmailToAddress { get { return Get<SettingString>("EmailToAddress"); } }
        public SettingString EmailToName { get { return Get<SettingString>("EmailToName"); } }

		public SettingBoolean SmtpConfigOverDb { get { return Get<SettingBoolean>("SmtpConfigOverDb"); } }
		public SettingString SmtpServer { get { return Get<SettingString>("SmtpServer"); } }
        public SettingString SmtpUserName { get { return Get<SettingString>("SmtpUserName"); } }
        public SettingString SmtpPassword { get { return Get<SettingString>("SmtpPassword"); } }

        public EmailSettings(SettingList settings) : base(settings, Type, TypeId)
        {
        }
    }
}
