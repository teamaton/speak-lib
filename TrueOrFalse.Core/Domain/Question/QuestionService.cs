using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.TrueOrFalse
{
    public class QuestionService
    {

        private IQuestionRepository _repository;

        public QuestionService(IQuestionRepository repository)
        {
            _repository = repository;
        }


        public QuestionList GetAll()
        {
            var result = SampleFactory.GetSampleQuestions();

            return result;
        }

        public void Create(Question question)
        {

            question.Created = question.Modified = DateTime.Now;
            
            _repository.Create(question);
        }

        public void Create(QuestionList questions)
        {
            foreach (var question in questions)
                Create(question);
        }

        public void Update(Question question)
        {
            question.Modified = DateTime.Now;

            _repository.Update(question);
        }

        public void Delete(Question question)
        {
            _repository.Delete(question);
        }
    }
}
