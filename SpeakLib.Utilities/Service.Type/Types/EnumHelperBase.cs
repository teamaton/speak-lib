using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace SpeakFriend.Utilities
{
    /// <summary>
    /// Automatically instantiates the internal list of values from the given Enum type T.
    /// </summary>
    /// <typeparam name="T">The Enum type that provides the values and strings.</typeparam>
    public class EnumHelperBase<T>
    {
        readonly protected EnumAnnotatedList _items = new EnumAnnotatedList();

        protected EnumHelperBase()
        {
            AddAllItemsOfType();
        }

        protected void AddAllItemsOfType()
        {
            var values = Enum.GetValues(typeof(T));
            foreach (var value in values)
            {
                _items.Add(new EnumAnnotated(value.ToString(), value));
            }
        }

        public DdlProxyEnum GetDdlProxy(DropDownList dropDownList)
        {
            return new DdlProxyEnum(dropDownList, _items);
        }

        public EnumAnnotated GetBy(Enum type)
        {
            foreach (var item in _items)
                if (item.Enum.Equals(type))
                    return item;

            return null;
        }

        public string GetNice(Enum type)
        {
            return GetBy(type).Name;
        }
    }
}
