using System.Reflection;
using Autofac;
using NHibernate;
using NHibernate.Cfg;
using SpeakFriend.Utilities;
using SpeakFriend.Utilities.Usefulness;
using Module = Autofac.Module;

namespace Tests.Usefulness.TestEnvironment
{
	public class Autofac_TestModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);

			builder.Register(c => new Configuration().Configure().BuildSessionFactory()).SingleInstance();
			builder.Register(c => c.Resolve<ISessionFactory>().OpenSession()).InstancePerLifetimeScope();

			builder.RegisterType<NHibernateHelperSF>().InstancePerLifetimeScope();

			builder.RegisterType<UsefulTestEntityService>().InstancePerLifetimeScope();
			builder.RegisterType<UsefulTestCreatorService>().InstancePerLifetimeScope();

			builder.RegisterType<UsefulnessService>().InstancePerLifetimeScope();
			builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
				.Where(t => typeof (IUsefulnessEntity).IsAssignableFrom(t));
		}
	}
}