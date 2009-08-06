namespace SpeakFriend.Utilities
{
    public class MessageOrderBy : OrderByCriteria
    {
        public OrderBy Id;
        public OrderBy From;
        public OrderBy To;
        public OrderBy Type;
        public OrderBy Status;
        public OrderBy DateCreated;

        public MessageOrderBy()
        {
            Id = new OrderBy("Id", this);
            From = new OrderBy("FromType", this);
            To = new OrderBy("ToType", this);
            Type = new OrderBy("Type", this);
            Status = new OrderBy("Status", this);
            DateCreated = new OrderBy("DateCreated", this);
        }
    }
}