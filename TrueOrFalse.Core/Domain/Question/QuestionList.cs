using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.TrueOrFalse
{
    public class QuestionList : List<Question>
    {
        public QuestionList()
        {

        }

        public QuestionList(IEnumerable<Question> list)
        {
            AddRange(list);
        }


    }
}
