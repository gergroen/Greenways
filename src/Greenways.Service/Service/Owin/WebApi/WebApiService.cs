using System;
using System.Web.Http;
using System.Web.Http.Dependencies;
using Common.Logging;
using Owin;
using Greenways.Service.Owin;

namespace Greenways.Service.WebApi
{
    public class WebApiService : IOwinService
    {
        private static readonly ILog Logger = LogManager.GetCurrentClassLogger();
        private readonly IDependencyResolver _dependencyResolver;
        private IDisposable _service;

        public int Order { get; set; }

        public WebApiService(IDependencyResolver dependencyResolver)
        {
            _dependencyResolver = dependencyResolver;
        }

        public void Configuration(IAppBuilder appBuilder)
        {
            Logger.Info("Configuring");

            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute("API Default", "api/{controller}/{action}", new { action = "Index" });
            config.DependencyResolver = _dependencyResolver;
            appBuilder.UseWebApi(config);

            Logger.Info("Configured");
        }

        public void Dispose()
        {
            Logger.Info("Disposing");

            if (_service != null)
            {
                _service.Dispose();
                _service = null;
            }

            Logger.Info("Disposed");
        }
    }
}