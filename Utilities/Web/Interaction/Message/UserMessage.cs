using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace SpeakFriend.Utilities.Web
{
    public class UserMessage
    {
        private string _Title;
        private readonly List<UserMessageItem> _Messages = new List<UserMessageItem>();

        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        public List<UserMessageItem> Messages
        {
            get { return _Messages; }
        }


        public UserMessage() { }

        public UserMessage(string title)
        {
            _Title = title;
        }

        public UserMessage(string title, string message)
            : this(title)
        {
            _Messages.Add(new UserMessageItem(message));
        }

        public UserMessage AddItem(string message, Control control)
        {
            _Messages.Add(new UserMessageItem(message, control));
            return this;
        }

        public UserMessage AddItem(string message, params Control[] controls)
        {
            _Messages.Add(new UserMessageItem(message, controls));
            return this;
        }

        public UserMessage AddItem(string message)
        {
            _Messages.Add(new UserMessageItem(message));
            return this;
        }

        public UserMessage SetTitle(string title)
        {
            _Title = title;
            return this;
        }

        public bool HasMessages()
        {
            return _Messages.Count > 0;
        }
    }
}
