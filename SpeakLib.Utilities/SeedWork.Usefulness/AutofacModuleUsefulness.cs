﻿using System;
using Autofac;
using Autofac.Builder;
using AutofacContrib.NHibernate;
using NHibernate.ByteCode.LinFu;
using NHibernate.Type;
using Environment=NHibernate.Cfg.Environment;

namespace SpeakFriend.Utilities.Usefulness
{
	public class AutofacModuleUsefulness : Module
	{
		public override void Configure(IContainer container)
		{
			base.Configure(container);

			Environment.BytecodeProvider = new AutofacBytecodeProvider(
				container, new ProxyFactoryFactory(), new DefaultCollectionTypeFactory());
		}

		protected override void Load(ContainerBuilder builder)
		{
			builder.Register<UsefulnessService>().ContainerScoped();

			builder.RegisterTypesMatching(t => typeof(IUsefulnessEntity).IsAssignableFrom(t)).FactoryScoped();
		}
	}
}
