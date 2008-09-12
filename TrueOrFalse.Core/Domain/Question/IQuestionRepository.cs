using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.TrueOrFalse
{
    public interface IQuestionRepository
    {
        void Create(Question question);
        void Update(Question question);
        void Delete(Question question);

        QuestionList GetAll();
        Question GetById(int questionId);
    }
}
