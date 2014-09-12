using System;
using Common.Logging;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Owin;

namespace Greenways.Service.SignalR
{
    public class SignalRService : IService
    {
        private static readonly ILog Logger = LogManager.GetCurrentClassLogger();
        private IDisposable _service;
        private readonly IDependencyResolver _dependencyResolver;

        public SignalRService(IDependencyResolver dependencyResolver)
        {
            _dependencyResolver = dependencyResolver;
        }

        public int Order { get; set; }
        public string Url { get; set; }

        public bool Start()
        {
            Logger.Info("Starting");

            try
            {
                var config = new HubConfiguration();
                config.Resolver = _dependencyResolver;
                _service = WebApp.Start(Url, x => Configuration(x, config));
            }
            catch (Exception ex)
            {
                Logger.Error("Error while starting SignalR service", ex);
                throw;
            }

            Logger.Info("Started");
            Logger.InfoFormat("Listening on {0}", Url);
            return true;
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

        private void Configuration(IAppBuilder app, HubConfiguration hubConfiguration)
        {
            app.UseCors(CorsOptions.AllowAll);
            app.RunSignalR(hubConfiguration);
        }
    }
}
