using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities
{
    /// <summary>
    /// Use EntitySettings as your base class if you want to handle settings for a specific entity.
    /// </summary>
    [Serializable]
    public class SettingList : PersistableList<Setting>
    {
        public SettingList()
        {
        }

        public SettingList(IEnumerable<Setting> list)
            : base(list)
        {
        }
    }
}
