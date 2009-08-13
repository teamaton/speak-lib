using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities
{
    public class StoredFile : IPersistable
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }

        public string EntityType { get; set; }
        public int EntityId { get; set; }

        public string OriginalFileName { get; set; }
    }
}
