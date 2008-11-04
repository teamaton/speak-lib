using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace CampingInfo.Core
{
    public class UserMessageItem
    {
        public string Text;

        /// <summary>
        /// Controls to highlight
        /// </summary>
        public List<Control> Controls = new List<Control>();

        public UserMessageItem(string text)
        {
            Text = text;
        }

        public UserMessageItem(string text, Control control)
        {
            Text = text;
            Controls.Add(control);
        }

        public UserMessageItem(string text, params Control[] controls)
        {
            Text = text;
            Controls.AddRange(controls);
        }

        public bool IsSetControl()
        {
            return Controls.Count > 0;
        }

    }
}
