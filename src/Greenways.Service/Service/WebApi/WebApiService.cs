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
        private readonly IDependencyResolver _dependencyResolver;
        private IDisposable _service;

        public int Order { get; set; }
        public string Url { get; set; }

        public WebApiService(IDependencyResolver dependencyResolver)
        {
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
                _service = WebApp.Start(Url, x => Configuration(x, config));
            }
            catch (Exception ex)
            {
                Logger.Error("Error while starting WebApi service", ex);
                throw;
            }

            Logger.Info("Started");
            Logger.InfoFormat("Listening on {0}", Url);
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