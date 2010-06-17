using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities
{
    /// <summary>
    /// Base class for all setting lists for specific entities.
    /// </summary>
	[Serializable]
	public class EntitySettingList
    {
        private readonly SettingList _settings;
        private readonly string _settingType;
        private readonly int _settingTypeId;

        public SettingList SettingList { get { return _settings; } }
        public string SettingType { get { return _settingType; } }
        public int SettingTypeId { get { return _settingTypeId; } }

        public EntitySettingList(SettingList settings, string settingType, int settingTypeId)
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
        /// Gets an existing Setting with the given key from the internal list or creates a new one, adds it to the list and returns it.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public T Get<T>(string key, string defaultValue) where T : Setting, new()
        {
            var setting = _settings.Find(s => s.Key.Equals(key));

            if (setting != null)
                return (T)setting;

            setting = new T { Key = key, ValueStr = defaultValue, SettingType = _settingType, SettingTypeId = _settingTypeId };

            _settings.Add(setting);

            return (T)setting;
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
