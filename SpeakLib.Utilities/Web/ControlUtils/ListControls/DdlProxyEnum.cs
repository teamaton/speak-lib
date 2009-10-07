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
    	private bool _setFirstItem;

        private const int NoSelection = -1;
        private readonly string _noSelectionValue = NoSelection.ToString();

        public DdlProxyEnum(DropDownList ddl, EnumAnnotatedList annotadedEnums)
        {
            _annotadedEnums = annotadedEnums;
            _ddl = ddl;
        }

		public DdlProxyEnum SetAddFirstItem(bool setFirstItem)
		{
			_setFirstItem = setFirstItem;
			return this;
		}

        public void Populate()
        {
            _ddl.Items.Clear();

			if (_setFirstItem)
				_ddl.Items.Add(new ListItem("- Alle -", NoSelection.ToString()));

            foreach(var item in _annotadedEnums)
            {
                _ddl.Items.Add(new ListItem(item.Name, item.Value.ToString()));
            }
        }

        public bool HasSelectedValue()
        {
            return _ddl.SelectedValue != _noSelectionValue;
        }

        public Enum GetSelectedType()
        {
            if(!HasSelectedValue())
                throw new Exception("no valid value: use 'HasSelectedValue()'");

            return _annotadedEnums.GetById(Convert.ToInt32(_ddl.SelectedValue)).Enum;
        }

        public void SetSelected(Enum @enum)
        {
            if (@enum == null)
                _ddl.SelectedValue = _noSelectionValue;
            else 
                _ddl.SelectedValue = new EnumAnnotated(@enum.ToString(), @enum).Value.ToString();
        }
    }
}
