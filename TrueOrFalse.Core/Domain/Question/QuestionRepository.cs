using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Criterion;

namespace SpeakFriend.TrueOrFalse
{
    public class QuestionRepository : RepositoryDb, IQuestionRepository
    {

        private readonly ISession _session;

        public QuestionRepository(ISession session) : base(session)
        {
            _session = session;
        }

        #region IQuestionRepository Members
        
        public void Create(Question question)
        {
            _session.Save(question);
        }

        public void Update(Question question)
        {
            _session.Update(question);
        }

        public void Delete(Question question)
        {
            _session.Delete(question);
        }

        public QuestionList GetAll()
        {
            return new QuestionList(_session.CreateCriteria(typeof(Question)).List<Question>());
        }

        public Question GetById(int questionId)
        {
            return _session.CreateCriteria(typeof(Question))
                           .Add(Restrictions.Eq("Id", questionId))
                           .UniqueResult<Question>();
        }

        #endregion
    }
}
