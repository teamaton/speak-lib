using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using SpeakFriend.Utilities;

namespace SpeakFriend.Utilities
{
    public abstract class MessageRepositoryDbBase<TMessage, TMessageList, TMessageParticipantType, TMessageType, TMessageStatus> 
        : RepositoryDb<TMessage, TMessageList> 
        where TMessage : MessageBase<TMessageParticipantType, TMessageType, TMessageStatus> 
        where TMessageList : MessageListBase<TMessage, TMessageParticipantType, TMessageType, TMessageStatus>, new()
    {
    	protected MessageRepositoryDbBase(ISession session)
            : base(session)
        {
        }
    }
}
