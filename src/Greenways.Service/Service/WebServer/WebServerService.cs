using System;
using Common.Logging;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.Hosting;
using Microsoft.Owin.StaticFiles;
using Owin;

namespace Greenways.Service.WebServer
{
    public class WebServerService : IService
    {
        private static readonly ILog Logger = LogManager.GetCurrentClassLogger();

        private IDisposable _service;

        public int Order { get; set; }
        public string Url { get; set; }
        public string WebDirectory { get; set; }

        public bool Start()
        {
            Logger.Info("Starting");

            var staticFileOptions = new StaticFileOptions
            {
                FileSystem = new PhysicalFileSystem(WebDirectory)
            };
            _service = WebApp.Start(Url, x => x.UseStaticFiles(staticFileOptions));

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
    }
}