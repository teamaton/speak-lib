using System;

namespace SpeakFriend.Utilities
{
    public abstract class MessageBase<TMessageParticipantType, TMessageType, TMessageStatus> : IPersistable
    {
        private DistributionType _distributionType;
        private TMessageStatus _status;
        public int Id { get; set; }
        public int FromId { get; set; }
        public TMessageParticipantType FromType { get; set; }

        /// <summary>
        /// As instantaneous data; could change over time for the given FromId/FromType. 
        /// </summary>
        public MessageAddress From { get; set; }

        public int ToId { get; set; }
        public TMessageParticipantType ToType { get; set; }

        /// <summary>
        /// As instantaneous data; could change over time for the given ToId/ToType.
        /// </summary>
        public MessageAddress To { get; set; }

        public TMessageType Type { get; set; }

        public DistributionType DistributionType
        {
            get { return _distributionType; }
            set { _distributionType = value; }
        }

        public string LanguageIso { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public bool Seen { get; set; }

        public TMessageStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }
    }
}