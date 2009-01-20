using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities
{
    /// <summary>
    /// Base class for all setting lists for specific entities. Use
    /// <code><see cref="AddEntitySetting"/></code>
    /// to add your custom settings to the underlying list.
    /// </summary>
    public class EntitySettings : SettingList
    {
        private readonly SettingType _settingType;
        private readonly int _settingTypeId;

        public SettingType SettingType { get { return _settingType; } }
        public int SettingTypeId { get { return _settingTypeId; } }

        public EntitySettings(SettingType type, int typeId)
        {
            _settingType = type;
            _settingTypeId = typeId;
        }

        /// <summary>
        /// Adds a setting to this list but changes SettingType and SettingTypeId to this
        /// EntitySetting's type and id.
        /// </summary>
        /// <param name="setting"></param>
        public void AddEntitySetting(Setting setting)
        {
            setting.SettingType = _settingType;
            setting.SettingTypeId = _settingTypeId;
            Add(setting);
        }
    }
}
