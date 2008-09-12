using System;
using Autofac;
using Autofac.Builder;
using NUnit.Framework;

namespace SpeakFriend.TrueOrFalse.Tests
{
    public abstract class DomainTestBase : AssertionHelper
    {
        private IContainer _rootContainer;
        protected IContainer _currentContainer { get; private set; }

        protected NHibernateHelper _nHibernateHelper { get { return Resolve<NHibernateHelper>(); } }

        protected QuestionService _questionService { get { return Resolve<QuestionService>(); } }
        
        private T Resolve<T>()
        {
            return _currentContainer.Resolve<T>();
        }
        
        public virtual void SetUp()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new CoreModule());
            _rootContainer = builder.Build();
            MakeNewContainer();
        }

        public virtual void TearDown()
        {
            DisposeContainer();
        }

        /// <summary>
        /// Wirft den Service-Container (einschließlich Inhalt) weg und erstellt einen neuen.
        /// Reinitialisiert alle Services, Sessions, etc.
        /// </summary>
        public void RecycleServiceContainer()
        {
            DisposeContainer();
            MakeNewContainer();
        }

        private void MakeNewContainer()
        {
            _currentContainer = _rootContainer.CreateInnerContainer();
        }

        private void DisposeContainer()
        {
            if (_currentContainer != null) _currentContainer.Dispose();
        }
    }
}