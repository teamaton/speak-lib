using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace SpeakFriend.Utilities
{
    [Serializable]
    public class MessageAddress
    {
        private MailAddress _mailAddress = null;

        public MessageAddress(){}

        public MessageAddress(string address)
        {
            Address = address;
        }

        public MessageAddress(string address, string displayName) : this(address)
        {
            DisplayName = displayName;
        }

        public MailAddress MailAddress
        {
            get
            {
                if (_mailAddress == null)
                    _mailAddress = new MailAddress(Address, DisplayName, Encoding.UTF8);

                return _mailAddress;
            }
        }

        public String Address { get; set; }

        private String _displayName = "";
        public String DisplayName
        {
            get { return _displayName; }
            set { _displayName = value; }
        }
    }
}
