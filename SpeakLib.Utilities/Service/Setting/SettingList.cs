using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities
{
    /// <summary>
    /// Use EntitySettings as your base class if you want to handle settings for a specific entity.
    /// </summary>
    public class SettingList : List<Setting>
    {
        public SettingList()
        {
        }

        public SettingList(IEnumerable<Setting> list)
        {
            AddRange(list);
        }

        public SettingString GetByValue(string value)
        {
            return (SettingString)Find(setting => setting is SettingString && (setting as SettingString).Value == value);
        }
    }
}