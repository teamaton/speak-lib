using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac.Builder;
using Autofac.Integration.Web;
using AutofacContrib.NHibernate;
using NHibernate.ByteCode.LinFu;
using SpeakFriend.Utilities;
using SpeakFriend.Utilities.Usefulness;
using Tests.Usefulness.TestEnvironment;

namespace Tests.Usefulness.TestEnvironment
{
    class Setup : BaseTest
    {
		public static void InitializeAll()
		{
			// Autofac
			var builder = new ContainerBuilder();
			builder.RegisterModule(new Autofac_TestModule());
//			var containerProvider = new ContainerProvider(builder.Build());
//
//			NHibernate.Cfg.Environment.BytecodeProvider = new AutofacBytecodeProvider(
//				containerProvider.ApplicationContainer, new ProxyFactoryFactory(),
//				new NHibernate.Type.DefaultCollectionTypeFactory());
		}

//        public static void CleanUp()
//        {
//            foreach (var type in new[] { typeof(UsefulEntity), typeof(UsefulnessEntry)})
//                _nHibernateHelper.EmptyTable(type);
//        }
//
//        public static void CreateTestEnvironment()
//        {
//            CleanUp();
//
//            //some random objects
//        	for (int i = 0;
//        	     i < 3;
//        	     i++)
//        	{
//        		
//        	}
//
//        	//some possible tags
//            foreach (var text in new[] { "Food", "Yummy", "Expensive", "I need it!", "Liquid" })
//            {
//            }
//
//        }
    }
}
