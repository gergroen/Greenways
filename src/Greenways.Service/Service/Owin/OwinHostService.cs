using Common.Logging;
using System;
using Microsoft.Owin.Hosting;
using Owin;
using System.Collections.Generic;
using System.Linq;

namespace Greenways.Service.Owin
{
    public class OwinHostService : IService
    {
        private static readonly ILog Logger = LogManager.GetCurrentClassLogger();

        private IDisposable _service;

        public int Order { get; set; }
        public string Url { get; set; }

        private readonly IList<IOwinService> _owinServices;

        public OwinHostService(IList<IOwinService> owinServices)
        {
            _owinServices = owinServices;
        }

        public bool Start()
        {
            Logger.Info("Starting");

            try
            {
                _service = WebApp.Start(Url, x => Configuration(x));
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

        private void Configuration(IAppBuilder appBuilder)
        {
            foreach (var owinService in _owinServices.OrderBy(x => x.Order))
            {
                owinService.Configuration(appBuilder);
            }
        }

        public bool Stop()
        {
            Logger.Info("Stopping");

            if (_service != null)
            {
                foreach (var owinService in _owinServices.OrderByDescending(x => x.Order))
                {
                    owinService.Dispose();
                }

                _service.Dispose();
                _service = null;
            }

            Logger.Info("Stopped");
            return true;
        }
    }
}
