﻿using System;
using System.Net.Http.Formatting;
using System.Web.Http;
using Owin;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using Test.It.While.Hosting.Your.Web.Application.Utils.Services;

namespace WebApi.Test.Application
{
    public class FooBarApplication : IApplication
    {
        public HttpConfiguration HttpConfiguration { get; } = new HttpConfiguration();

        // ReSharper disable once UnusedMember.Global Used by Owin host
        public void Configuration(IAppBuilder appBuilder) => Configuration(appBuilder, container => { });

        public void Configuration(IAppBuilder appBuilder, Action<Container> reconfigure)
        {
            ConfigureApplication(reconfigure);
            appBuilder.UseWebApi(HttpConfiguration);
        }

        private void ConfigureApplication(Action<Container> reconfigure)
        {
            var container = ConfigureContainer(reconfigure);
            HttpConfiguration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            HttpConfiguration.Formatters.Clear();
            HttpConfiguration.Formatters.Add(new JsonMediaTypeFormatter());

            HttpConfiguration.MapHttpAttributeRoutes();
        }

        private static Container ConfigureContainer(Action<Container> reconfigure)
        {
            var container = new Container();

            container.RegisterSingleton<IService, Service>();

            reconfigure(container);
            container.Verify();
            return container;
        }
    }
}