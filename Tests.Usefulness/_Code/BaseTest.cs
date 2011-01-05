using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Autofac.Builder;
using NUnit.Framework;
using SpeakFriend.Utilities;
using SpeakFriend.Utilities.Usefulness;
using SpeakFriend.Utilities.Web;
using Tests.Usefulness.TestEnvironment;

namespace Tests.Usefulness
{
    public class BaseTest : AssertionHelper
    {
    	protected NHibernateHelperSF _nHibernateHelper { get { return Resolve<NHibernateHelperSF>(); } }
		protected UsefulnessService _usefulnessService { get { return Resolve<UsefulnessService>(); } }
		protected UsefulTestEntity _usefulTestEntity { get { return Resolve<UsefulTestEntity>(); } }
		protected UsefulTestEntityService _usefulTestEntityService { get { return Resolve<UsefulTestEntityService>(); } }
		protected UsefulTestCreatorService _usefulTestCreatorService { get { return Resolve<UsefulTestCreatorService>(); } }

		private IContainer _rootContainer;
		protected ILifetimeScope _currentContainer { get; private set; }
		private bool _setUp;
		private bool _testFixtureSetUp;
		private bool _testFixtureTornDown;
        
		[SetUp]
		public virtual void SetUp()
		{
			_setUp = true;
			RecycleServiceContainerAndClearCache();
		}

		[TestFixtureSetUp]
		public virtual void TestFixtureSetUp()
		{
			_testFixtureSetUp = true;
			InitializeContainer();
		}

		[TestFixtureTearDown]
		public virtual void TestFixtureTearDown()
		{
			_testFixtureTornDown = true;
			DisposeContainer();
		}

		~BaseTest()
		{
			if (!_setUp)
				throw new Exception("If you want to use SetUp() from NUnit, override the method from the BaseTest class!");
			// Wenn man hier ankommt, weil man beim Debuggen eine Exception bekommen hat, kann man das ignorieren.
			if (!_testFixtureTornDown)
				throw new Exception("Override TestFixtureTearDown() from BaseTest if you want to use [TestFixtureTearDown]!");
		}

		protected T Resolve<T>()
		{
			return _currentContainer.Resolve<T>();
		}

		protected T Resolve<T>(params NamedParameter[] parameters)
		{
			return _currentContainer.Resolve<T>(parameters);
		}

		/// <summary>
		/// Wirft den Service-Container (einschließlich Inhalt) weg und erstellt einen neuen.
		/// Reinitialisiert alle Services, Sessions, etc.
		/// </summary>
		public virtual void RecycleServiceContainer()
		{
			DisposeContainer();
			MakeNewContainer();
		}

    	public virtual void RecycleServiceContainerAndClearCache()
		{
			Console.WriteLine("BaseTest::RecycleServiceContainerAndClearCache");
			RecycleServiceContainer();
			Cache.Clear();
		}

		private ILifetimeScope MakeNewContainer()
		{
			_currentContainer = _rootContainer.BeginLifetimeScope();
			return _currentContainer;
		}

		private void InitializeContainer()
		{
			var builder = new ContainerBuilder();
			builder.RegisterModule(new Autofac_TestModule());

			_rootContainer = builder.Build();
			MakeNewContainer();
		}

    	protected void DisposeContainer()
		{
			ClearSessions();

			if (_currentContainer != null)
				_currentContainer.Dispose();
		}

		protected virtual void ClearSessions()
		{
			
		}
    }
}
