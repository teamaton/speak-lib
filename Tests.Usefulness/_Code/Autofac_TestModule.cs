using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Autofac;
using Autofac.Builder;
using Autofac.Core;
using NHibernate;
using NHibernate.ByteCode.LinFu;
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
			builder.RegisterSource(new UsefulnessSource());
			//IUsefulnessEntity
		}
	}

	internal class UsefulnessSource : IRegistrationSource
	{
		public IEnumerable<IComponentRegistration> RegistrationsFor(Service service, Func<Service, IEnumerable<IComponentRegistration>> registrationAccessor)
		{
			var ts = service as TypedService;
			if (ts != null && !ts.ServiceType.IsAbstract && ts.ServiceType.IsClass
				&& typeof(IUsefulnessEntity).IsAssignableFrom(ts.ServiceType))
			{
				var rb = RegistrationBuilder.ForType(ts.ServiceType);
				return new[] { rb.CreateRegistration() };
			}

			return Enumerable.Empty<IComponentRegistration>();
		}

		public bool IsAdapterForIndividualComponents
		{
			get { return false; }
		}
	}
}
