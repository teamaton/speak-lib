using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac.Builder;
using SpeakFriend.Utilities;

namespace SpeakFriend.ExampleApp.Core
{
    public class Autofac_CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            Setting(builder);
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
