using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace SpeakFriend.Utilities
{
    public class SettingSearchFilter : ConditionContainer
    {
        public ConditionInteger SettingTypeId { get; private set;}
        public ConditionString SettingType { get; private set; }
        public ConditionString Key { get; private set; }
        public ConditionDisjunction<int> CatalogIds;

        public SettingSearchFilter()
        {
            SettingTypeId = new ConditionInteger(this, "SettingTypeId");
            SettingType = new ConditionString(this, "SettingType");
            Key = new ConditionString(this, "Key");
            CatalogIds = new ConditionDisjunction<int>(this, "Catalog.Id");
        }

    }
}
