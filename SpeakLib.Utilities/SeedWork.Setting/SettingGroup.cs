using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities
{
    public class SettingGroup
    {
        private readonly SettingList _settings;
        private readonly SettingType _settingType;
        private readonly int _settingTypeId;

        public SettingList SettingList { get { return _settings; } }
        public SettingType SettingType { get { return _settingType; } }
        public int SettingTypeId { get { return _settingTypeId; } }

        /// <summary>
        /// Ignore SettingType and SettingTypeId for now. All settings are used for this app (one shop).
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="settingType"></param>
        /// <param name="settingTypeId"></param>
        public SettingGroup(SettingList settings, SettingType settingType, int settingTypeId)
        {
            _settings = settings;
            _settingType = settingType;
            _settingTypeId = settingTypeId;
        }

        /// <summary>
        /// Gets an existing Setting with the given key from the internal list or creates a new one, adds it to the list and returns it.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get<T>(string key) where T : Setting, new()
        {
            var setting = _settings.Find(s => s.Key.Equals(key));

            if (setting != null)
                return (T) setting;

            setting = new T {Key = key, SettingType = _settingType, SettingTypeId = _settingTypeId};

            _settings.Add(setting);

            return (T) setting;
        }

        /// <summary>
        /// Gets an existing Setting with the given key from the internal list or null if no such setting exists.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T TryGet<T>(string key) where T : Setting
        {
            var setting = _settings.Find(s => s.Key.Equals(key));

            if (setting != null)
                return (T)setting;

            return null;
        }
    }
}
