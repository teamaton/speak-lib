using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.TrueOrFalse
{
    public class Question
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public Answer Answer { get; set; }
        public Source Source { get; set; }

        public DateTime Modified { get; set; }
        public DateTime Created { get; set; }
    }
}
