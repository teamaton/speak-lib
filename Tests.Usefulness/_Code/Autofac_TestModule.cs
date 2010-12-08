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
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);

			builder.Register(c => new Configuration().Configure().BuildSessionFactory()).SingletonScoped();
			builder.Register(c => c.Resolve<ISessionFactory>().OpenSession()).ContainerScoped();

			builder.Register<NHibernateHelperSF>().ContainerScoped();

			builder.Register<UsefulTestEntityService>().ContainerScoped();
			builder.Register<UsefulTestCreatorService>().ContainerScoped();

			builder.Register<UsefulnessService>().ContainerScoped();
			builder.RegisterTypesMatching(t => typeof(IUsefulnessEntity).IsAssignableFrom(t)).FactoryScoped();
		}
	}
}
