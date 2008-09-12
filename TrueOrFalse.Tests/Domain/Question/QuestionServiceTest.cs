using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
    

namespace SpeakFriend.TrueOrFalse.Tests
{
    [TestFixture]
    public class QuestionServiceTest : DomainTestBase
    {
        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            _nHibernateHelper.TruncateAll();
        }

        [TearDown]
        public override void TearDown()
        {
            base.TearDown();
        }


        [Test]
        public void CRUD()
        {
            // Create
            var questions = SampleFactory.GetSampleQuestions();
            _questionService.Create(questions);

            RecycleServiceContainer();

            // Retrieve
            var questionsRetrieved = _questionService.GetAll();

            // Das ist das Gleiche
            Assert.AreEqual(questions.Count, questionsRetrieved.Count); 
            Expect(questionsRetrieved.Count, EqualTo(questions.Count));

            Expect(
                questions.All(
                    question => questionsRetrieved.Any(
                        questionRetrieved => questionRetrieved.Text == question.Text)));

            RecycleServiceContainer();

            //Update
            questions[0].Text = "Sind Sie ein Meerschweinchen?";
            _questionService.Update(questions[0]);
            RecycleServiceContainer();
            

            //Delete
            
        }
    }
}
