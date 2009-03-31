using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace SpeakFriend.Utilities
{
    public class SettingOrderBy : OrderByCriteria
    {
                
    }


    public class SettingSearchDesc : Pager 
    {
        private SettingSearchFilter _filter = new SettingSearchFilter();
        public SettingSearchFilter Filter { get { return _filter ?? (_filter = new SettingSearchFilter()); } }

        private readonly SettingOrderBy _orderBy = new SettingOrderBy();
        public SettingOrderBy OrderBy { get { return _orderBy; } }

        public SettingSearchDesc()
        {
            QueryAll = true;
        }

        /// <summary>
        /// Erstellt einen neuen <see cref="SettingSearchDesc"/> und setzt gleich den Filter auf die
        /// übergebenen Werte.
        /// </summary>
        public SettingSearchDesc(string settingType, int settingTypeId) : this()
        {
            Filter.SettingType.EqualTo(settingType);
            Filter.SettingTypeIds.Add(settingTypeId);
        }
    }
}
