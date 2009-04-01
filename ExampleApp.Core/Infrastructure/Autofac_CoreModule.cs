using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac.Builder;
using NHibernate.Cfg;
using SpeakFriend.Utilities;

namespace SpeakFriend.ExampleApp.Core
{
    public class Autofac_CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            NHibernate(builder);
            Setting(builder);
        }

        private void NHibernate(ContainerBuilder builder)
        {
            builder
                .Register(c => new SessionFactoryContainer(() => new Configuration().Configure().BuildSessionFactory()))
                .As<SessionFactoryContainer>()
                .SingletonScoped();

            builder
                .Register(c =>
                    new SessionManager(c.Resolve<SessionFactoryContainer>().GetSessionFactory().OpenSession()))
                .As<SessionManager>()
                .ContainerScoped();
        }

        private void Setting(ContainerBuilder builder)
        {
            builder
                .Register(c => new SettingRepository(c.Resolve<SessionManager>().Session))
                .As<ISettingRepository>()
                .ContainerScoped();

            builder
                .Register(c => new SettingService(c.Resolve<ISettingRepository>()))
                .As<SettingService>()
                .ContainerScoped();
        }

    }
}
