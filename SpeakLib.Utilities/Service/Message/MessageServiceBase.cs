using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace SpeakFriend.Utilities
{
    public class MessageServiceBase<TMessage, TMessageList, TMessageParticipantType, TMessageType, TMessageStatus>
        where TMessage : MessageBase<TMessageParticipantType, TMessageType, TMessageStatus> 
        where TMessageList : MessageListBase<TMessage, TMessageParticipantType, TMessageType, TMessageStatus>, new()
    {
        private readonly EmailService _emailService;
        private readonly MessageRepositoryDbBase<TMessage, TMessageList, TMessageParticipantType, TMessageType, TMessageStatus> _messageRepositoryDb;
        private readonly ISession _session;

        public MessageServiceBase(
            EmailService emailService, 
            MessageRepositoryDbBase<TMessage, TMessageList, TMessageParticipantType, TMessageType, TMessageStatus> messageRepositoryDb,
            ISession session)
        {
            _emailService = emailService;
            _messageRepositoryDb = messageRepositoryDb;
            _session = session;
        }

        private void Create(TMessage message)
        {
            message.DateCreated = DateTime.Now;
            message.DateModified = DateTime.Now;

            _session.Save(message);
        }


        public void Send(TMessage message)
        {
            if (Convert.ToInt32(message.Type) == 0)
                throw new Exception("Message.Type needs to be set (value 0 is invalid)");

            if (Convert.ToInt32(message.FromType) == 0)
                throw new Exception("Message.FromType needs to be set (value 0 is invalid)");

            if (Convert.ToInt32(message.ToType) == 0)
                throw new Exception("Message.ToType needs to be set (value 0 is invalid)");

            if (message.DistributionType == 0)
                throw new Exception("Message.DistributionType needs to be set (value 0 is invalid)");

            if (!Enum.IsDefined(typeof(TMessageStatus), message.Status)) //. Convert.ToInt32(message.Status) == 0)
                throw new Exception("Message.Status needs to be set (value 0 is invalid)");

            Create(message);

            if (message.DistributionType == DistributionType.Email)
                _emailService.Send(message);
        }

        public TMessageList GetAll()
        {
            var list = new TMessageList();
            list.AddRange(_session.CreateCriteria(typeof (TMessage)).List<TMessage>());
            return list;
        }

        public TMessageList GetUnseen()
        {
            var unseen = new TMessageList();
            unseen.AddRange(
                _session.CreateCriteria(typeof (TMessage))
                    .Add(Restrictions.Eq("Seen", false))
                    .List<TMessage>());
            return unseen;
        }

        public TMessageList GetBy(MessageSearchDesc searchDesc)
        {
            return _messageRepositoryDb.GetBy(searchDesc);
        }
    }
}
