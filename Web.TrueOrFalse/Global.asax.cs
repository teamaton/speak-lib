using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Autofac.Builder;
using Autofac.Integration.Web;
using SpeakFriend.TrueOrFalse;

namespace Web.TrueOrFalse
{
    public class Global : System.Web.HttpApplication, IContainerProviderAccessor
    {

        protected void Application_Start(object sender, EventArgs e)
        {
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new CoreModule());
            _containerProvider = new ContainerProvider(builder.Build());
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        static IContainerProvider _containerProvider;
        public static IContainerProvider ContainerProvider
        {
            get { return _containerProvider; }
        }

        IContainerProvider IContainerProviderAccessor.ContainerProvider
        {
            get { return ContainerProvider; }
        }
    }
}