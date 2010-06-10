using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpeakFriend.Utilities;

namespace SpeakFriend.Utilities
{
    [Serializable]
    public class MessageSearchFilter : ConditionContainer
    {
        public ConditionInteger Id;
        public ConditionEnum MessageType;
        public ConditionEnum MessageStatus;

        public ConditionEnum ToType;
        public ConditionEnum FromType;

        public ConditionBoolean Seen;

        public MessageSearchFilter()
        {
            Id = new ConditionInteger(this, "Id");
            MessageType = new ConditionEnum(this, "Type");
            MessageStatus = new ConditionEnum(this, "Status");

            ToType = new ConditionEnum(this, "ToType");
            FromType = new ConditionEnum(this, "FromType");

            Seen = new ConditionBoolean(this, "Seen");
        }
    }
}
