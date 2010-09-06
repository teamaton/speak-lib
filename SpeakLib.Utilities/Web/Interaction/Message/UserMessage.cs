﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace SpeakFriend.Utilities.Web
{
    [Serializable]
    public class UserMessage
    {
        private string _title;
        private readonly List<UserMessageItem> _messages = new List<UserMessageItem>();
    	
    	public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        public List<UserMessageItem> Messages
        {
            get { return _messages; }
        }

        public UserMessage() { }

        public UserMessage(string title)
        {
            _title = title;
        }

        public UserMessage(string title, string message)
            : this(title)
        {
            _messages.Add(new UserMessageItem(message));
        }

    	public UserMessage AddItem(string message)
        {
            _messages.Add(new UserMessageItem(message));
            return this;
        }

    	public UserMessage AddItem(Control control)
    	{
    		_messages.Add(new UserMessageItem(control));
    		return this;
    	}

    	public UserMessage SetTitle(string title)
        {
            _title = title;
            return this;
        }

        public bool HasMessages()
        {
            return _messages.Count > 0;
        }

        public override string ToString()
        {
            if (HasMessages())
                return Messages[0].Text;
            
            return !string.IsNullOrEmpty(Title) ? Title : "empty";
        }

    	public List<Control> RenderMessageBody(UserMessageRenderMode renderMode)
    	{
			if (renderMode == UserMessageRenderMode.Raw)
			{
				var list = new List<Control>();
				foreach (var messageItem in Messages)
					list.AddRange(messageItem.Controls);
				return list;
			}

    		if (renderMode == UserMessageRenderMode.Paragraph || Messages.Count == 1)
			{
				return Messages.Select(m=>m.ToControl(new HtmlGenericControl("p"))).ToList();
			}
    		
			if (renderMode == UserMessageRenderMode.List)
    		{
    			var msgList = new HtmlGenericControl("ul");

    			foreach (UserMessageItem messageItem in Messages)
    				msgList.Controls.Add(messageItem.ToControl(new HtmlGenericControl("li")));

    			return new List<Control> {msgList};
    		}

			throw new ArgumentOutOfRangeException("renderMode");
    	}
    }
}
