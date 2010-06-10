using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities
{
    [Serializable]
    public class MessageListBase<TMessage, TMessageParticipantType, TMessageType, TMessageStatus>
        : PersistableList<TMessage> where TMessage : MessageBase<TMessageParticipantType, TMessageType, TMessageStatus>
    {
        public MessageListBase() { }

        public MessageListBase(IEnumerable<TMessage> messages)
        {
            AddRange(messages);
        }
    }
}
