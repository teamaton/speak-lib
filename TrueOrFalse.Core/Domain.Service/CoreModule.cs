using Autofac.Builder;
using Autofac.Registrars.Delegate;
using NHibernate;
using NHibernate.Cfg;
using Autofac.Component;
using Autofac;

namespace SpeakFriend.TrueOrFalse
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            NHibernate(builder);
            NHibernateHelperForTests(builder);
            Question(builder);
        }

        private void Question(ContainerBuilder builder)
        {
            builder
                .Register(c => new QuestionService(c.Resolve<IQuestionRepository>()))
                .As<QuestionService>()
                .ContainerScoped();

            builder
                .Register(c => new QuestionRepository(c.Resolve<ISession>()))
                .As<IQuestionRepository>()
                .ContainerScoped();
        }

        private void NHibernate(ContainerBuilder builder)
        {
            builder
                .Register(c => new Configuration().Configure().BuildSessionFactory())
                .As<ISessionFactory>()
                .SingletonScoped();

            builder
                .Register(c => new SessionManager(c.Resolve<ISessionFactory>().OpenSession()))
                .As<SessionManager>()
                .ContainerScoped();

            builder
                .Register(c => c.Resolve<SessionManager>().Session)
                .As<ISession>()
                .ContainerScoped();
        }

        private void NHibernateHelperForTests(ContainerBuilder builder)
        {
            builder
                .Register(c => new NHibernateHelper(c.Resolve<SessionManager>().Session))
                .As<NHibernateHelper>()
                .ContainerScoped();
        }

    }
}