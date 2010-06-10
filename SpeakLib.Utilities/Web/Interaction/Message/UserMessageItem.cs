using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SpeakFriend.Utilities.Web
{
    [Serializable]
    public class UserMessageItem
    {
		private readonly Literal _literal = new Literal();
    	public string Text
    	{
    		get { return _literal.Text; } 
			set { _literal.Text = value; }
    	}

        /// <summary>
        /// Arbitrary controls to show inside this list item.
        /// </summary>
        public List<Control> Controls = new List<Control>();

        public UserMessageItem(string text)
        {
            Text = text;
			Controls.Add(_literal);
        }

        public UserMessageItem(params Control[] controls)
        {
            Controls.AddRange(controls);
        }

		public Control ToControl(Control container)
		{
			foreach (var control in Controls)
				container.Controls.Add(control);

			return container;
		}
    }
}
