using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace SpeakFriend.Utilities.Web
{
	public static class DdlPopulator
	{
		public static DropDownList Populate<T>(this DropDownList ddl)
		{
			ddl.Items.Clear();
			foreach (var value in Enum<T>.GetValues())
			{
				ddl.Items.Add(new ListItem(value.ToString(), Enum<T>.ParseToIntString(value)));
			}
			return ddl;
		}

		public static DropDownList SelectedEnumValue<T>(this DropDownList ddl, T value)
		{
			ddl.ClearSelection();

			if (ddl.Items.Count <= 0)
				return ddl;

			var listItem = ddl.Items.FindByValue(Enum<T>.ParseToIntString(value));
			if (listItem == null)
				return ddl;

			listItem.Selected = true;
			return ddl;
		}

		public static T SelectedEnumValue<T>(this DropDownList ddl)
		{
			return Enum<T>.Parse(ddl.SelectedValue);
		}
	}
}
