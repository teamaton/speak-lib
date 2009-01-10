using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities
{
    public enum SettingType
    {
        Campsite = 1,
        Admin = 2,
        User = 3,
        Email = 4,
        TranslationLogin = 5,
    }

    public class Setting
    {
        protected object _default;

        public virtual int Id { get; set; }

        public virtual string Key { get; set; }
        public virtual string ValueStr { get; set; }

        /// <summary>
        /// The entity type (e.g. Company) to which this Setting belongs.
        /// </summary>
        public virtual SettingType SettingType { get; set; }

        /// <summary>
        /// The ID of the entity to which this setting belongs.
        /// </summary>
        public virtual int SettingTypeId { get; set; }

        public virtual DateTime DateCreated { get; set; }
        public virtual DateTime DateModified { get; set; }

        /// <summary>
        /// This c'tor is used by generic methods to create a new Setting.
        /// Set the key to null for early detection of misuse.
        /// </summary>
        protected Setting() : this(null)
        {
        }

        public Setting(string key) : this(key, String.Empty)
        {
        }

        public Setting(string key, object defaultValue)
        {
            Key = key;
            _default = defaultValue ?? "";
            ValueStr = _default.ToString();
        }

        public virtual bool IsDefault()
        {
            return _default.Equals(ValueStr);
        }
    }
}
