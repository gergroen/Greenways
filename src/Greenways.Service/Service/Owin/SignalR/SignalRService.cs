using System;
using Common.Logging;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Owin;
using Greenways.Service.Owin;

namespace Greenways.Service.SignalR
{
    public class SignalRService : IOwinService
    {
        private static readonly ILog Logger = LogManager.GetCurrentClassLogger();
        private IDisposable _service;
        private readonly IDependencyResolver _dependencyResolver;

        public SignalRService(IDependencyResolver dependencyResolver)
        {
            _dependencyResolver = dependencyResolver;
        }

        public int Order { get; set; }

        public void Configuration(IAppBuilder appBuilder)
        {
            Logger.Info("Configuring");

            var hubConfiguration = new HubConfiguration();
            hubConfiguration.Resolver = _dependencyResolver;
            appBuilder.MapSignalR(hubConfiguration);

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
