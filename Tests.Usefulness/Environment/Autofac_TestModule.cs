using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Autofac.Builder;
using AutofacContrib.NHibernate;
using NHibernate;
using NHibernate.ByteCode.LinFu;
using NHibernate.Cfg;
using SpeakFriend.Utilities;
using SpeakFriend.Utilities.Usefulness;

namespace Tests.Usefulness.TestEnvironment
{
	public class Autofac_TestModule : Module
	{
		public override void Configure(IContainer container)
		{
			base.Configure(container);

			NHibernate.Cfg.Environment.BytecodeProvider = new AutofacBytecodeProvider(
				container, new ProxyFactoryFactory(), new NHibernate.Type.DefaultCollectionTypeFactory());
		}

		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);

			builder.Register(c => new Configuration().Configure().BuildSessionFactory()).SingletonScoped();
			builder.Register(c => c.Resolve<ISessionFactory>().OpenSession()).ContainerScoped();

			builder.Register<UsefulnessService>().ContainerScoped();

			builder.Register<UsefulnessValue>().FactoryScoped();
			builder.RegisterGeneratedFactory<UsefulnessValue.Factory>();
			
			builder.Register<UsefulEntity>().FactoryScoped();
			builder.Register<UsefulEntityService>().ContainerScoped();

			builder.Register<NHibernateHelperSF>().ContainerScoped();
		}
	}
}
