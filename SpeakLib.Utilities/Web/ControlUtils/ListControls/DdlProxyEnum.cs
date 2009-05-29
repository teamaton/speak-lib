using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace SpeakFriend.Utilities
{
    public class DdlProxyEnum
    {
        private readonly EnumAnnotatedList _annotadedEnums;
        private readonly DropDownList _ddl;

        private const int _noSelection = -1;

        public DdlProxyEnum(DropDownList ddl, EnumAnnotatedList annotadedEnums)
        {
            _annotadedEnums = annotadedEnums;
            _ddl = ddl;
        }

        public void Populate()
        {
            _ddl.Items.Clear();
            _ddl.Items.Add(new ListItem("- Alle -", _noSelection.ToString()));

            foreach(var item in _annotadedEnums)
            {
                _ddl.Items.Add(new ListItem(item.Name, item.Value.ToString()));
            }
        }

        public bool HasSelectedValue()
        {
            return Convert.ToInt32(_ddl.SelectedValue) != -1;
        }

        public Enum GetSelectedType()
        {
            if(!HasSelectedValue())
                throw new Exception("no valid value: use 'HasSelectedValue()'");

            return _annotadedEnums.GetById(Convert.ToInt32(_ddl.SelectedValue)).Enum;
        }
    }
}
