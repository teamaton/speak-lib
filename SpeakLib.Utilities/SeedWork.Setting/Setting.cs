﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities
{
    public class Setting : ICloneable
    {
        protected object _default;

        public virtual int Id { get; set; }

        public virtual string Key { get; set; }
        public virtual string ValueStr { get; set; }

        /// <summary>
        /// The entity type (e.g. Company) to which this Setting belongs.
        /// </summary>
        public virtual string SettingType { get; set; }

        /// <summary>
        /// The ID of the entity to which this setting belongs.
        /// </summary>
        public virtual int SettingTypeId { get; set; }

        public virtual DateTime Created { get; set; }
        public virtual DateTime Modified { get; set; }

        /// <summary>
        /// This c'tor is used by generic methods to create a new Setting.
        /// Set the key to null for early detection of misuse.
        /// </summary>
        public Setting() : this(null)
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

        object ICloneable.Clone()
        {
            return Clone();
        }

        public virtual Setting Clone()
        {
            return Clone<Setting>();
        }

        protected T Clone<T>() where T : Setting, new()
        {
            return new T
                       {
                           _default = _default,
                           Created = Created,
                           Id = Id,
                           Key = Key,
                           Modified = Modified,
                           SettingType = SettingType,
                           SettingTypeId = SettingTypeId,
                           ValueStr = ValueStr
                       };
        }
    }
}
