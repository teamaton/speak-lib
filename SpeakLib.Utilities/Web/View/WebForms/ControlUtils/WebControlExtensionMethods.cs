using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Iesi.Collections.Generic;
using SpeakFriend.Utilities;

namespace SpeakFriend.Utilities.Web
{
	public static class WebControlExtensionMethods
	{
		public static WebControl Title(this WebControl control, string title)
		{
			control.Attributes["title"] = title ?? "";
			return control;
		}

		/// <summary>
		/// Adds the given class to the CssClass member of the given control.
		/// </summary>
		public static void AddCssClass(this WebControl control, string classToAdd)
		{
			if (ControlUtil.ContainsCssClass(control.CssClass, classToAdd))
				return;

			control.CssClass = ControlUtil.GetNewCssString(control.CssClass, classToAdd);
		}

		public static bool IsContentItem(this RepeaterItem item)
		{
			return item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem;
		}

		public static bool IsContentItem(this DataGridItem item)
		{
			return item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem;
		}

		public static bool IsContentItem(this DataListItem item)
		{
			return item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem;
		}

		public static bool IsHeaderItem(this RepeaterItem item)
		{
			return item.ItemType == ListItemType.Header;
		}

		public static bool IsAlternatingItem(this RepeaterItem item)
		{
			return item.ItemType == ListItemType.AlternatingItem;
		}

		public static T Find<T>(this Control control, string controlName, bool throwIfNotFound = true) where T : Control
		{
			var item = (T) control.FindControl(controlName);
			if (item == null && throwIfNotFound)
				throw new Exception("The controlName:'" + controlName + "' was not found");
			return item;
		}

		/// <summary>
		/// Returns all controls of the given Type that are found inside this control.
		/// Searches recursively.
		/// </summary>
		public static IEnumerable<T> Controls<T>(this Control control) where T : Control
		{
			var controls = control.Controls;

			if (controls.Count == 0) return new List<T>(0);

			var newColl = new HashedSet<T>();
			foreach (Control child in controls)
			{
				if (child is T)
					newColl.Add((T) child);

				var childColl = child.Controls<T>();
				foreach (T ctrl in childColl)
					newColl.Add(ctrl);
			}

			return newColl;
		}

		/// <summary>
		/// Returns all controls of the given Type that are found inside this control.
		/// Searches recursively.
		/// </summary>
		public static IEnumerable<Control> ControlsOfType(this Control control, List<Type> types,
		                                                  params Type[] ignoreAllOfAndBelow)
		{
			var controls = control.Controls;

			if (controls.Count == 0)
				return new List<Control>(0);

			var newColl = new HashedSet<Control>();

			foreach (Control child in controls)
			{
				var childType = child.GetType();

				if (ignoreAllOfAndBelow.Any(t => t.IsAssignableFrom(childType)))
					continue;

				if (types.Any(t => t == childType))
					newColl.Add(child);

				var childColl = child.ControlsOfType(types, ignoreAllOfAndBelow);
				foreach (Control ctrl in childColl)
					newColl.Add(ctrl);
			}

			return newColl;
		}

		public static Control FirstParentOfType<T>(this Control control)
		{
			var parent = control.Parent;
			while (parent != null && !(parent is T))
				parent = parent.Parent;
			return parent;
		}
	}
}