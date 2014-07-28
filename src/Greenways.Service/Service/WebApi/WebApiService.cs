using System;
using System.Web.Http;
using System.Web.Http.Dependencies;
using Common.Logging;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Owin;

namespace Greenways.Service.WebApi
{
    public class WebApiService : IService
    {
        private static readonly ILog Logger = LogManager.GetCurrentClassLogger();
        private readonly string _url;
        private readonly IDependencyResolver _dependencyResolver;
        private IDisposable _service;

        public int Order { get; private set; }

        public WebApiService(int order, string url, IDependencyResolver dependencyResolver)
        {
            Order = order;
            _url = url;
            _dependencyResolver = dependencyResolver;
        }

        public bool Start()
        {
            Logger.Info("Starting");

            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute("API Default", "{controller}/{action}", new { action = "Index" });
            config.DependencyResolver = _dependencyResolver;

            try
            {
                _service = WebApp.Start(_url, x => Configuration(x,config));
            }
            catch (Exception ex)
            {
                Logger.Error("Error while starting WebApi service", ex);
                throw;
            }

            Logger.Info("Started");
            Logger.InfoFormat("Listening on {0}", _url);
            return true;
        }

        private void Configuration(IAppBuilder appBuilder, HttpConfiguration config)
        {
            appBuilder.UseCors(CorsOptions.AllowAll);
            appBuilder.UseWebApi(config);
        }

        public bool Stop()
        {
            Logger.Info("Stopping");

            if (_service != null)
            {
                _service.Dispose();
                _service = null;
            }

            Logger.Info("Stopped");
            return true;
        }
    }
}