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
       
        public MessageRepositoryDbBase(ISession session)
            : base(session)
        {
        }

        public TMessageList GetBy(MessageSearchDesc searchDesc)
        {
            var criteria = GetExecutableCriteria();

            AddConditions(searchDesc, criteria);
            AddOrderBy(criteria, searchDesc.OrderBy);

            SetTotalItemCount(criteria, searchDesc);
            SetPager(criteria, searchDesc);

            var result = new TMessageList();
            result.AddRange(criteria.List<TMessage>());
            return result;

        }

        private void AddConditions(MessageSearchDesc searchDesc, ICriteria criteria)
        {
            AddGenericConditions(criteria, searchDesc.Filter);
        }
    }
}
