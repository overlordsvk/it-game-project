﻿using BL.Config;
using Castle.Windsor;
using GameWebMVC.App_Start.Windsor;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace GameWebMVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static readonly IWindsorContainer Container = new WindsorContainer();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            BootstrapContainer();
        }

        private void BootstrapContainer()
        {
            // configure DI
            Container.Install(new BLInstaller());
            Container.Install(new GameWebMVCInstaller());

            // set controller factory
            var controllerFactory = new WindsorControllerFactory(Container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                if (authTicket != null && !authTicket.Expired)
                {
                    var roles = authTicket.UserData.Split(',');
                    HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(new FormsIdentity(authTicket), roles);
                }
            }
        }
    }
}