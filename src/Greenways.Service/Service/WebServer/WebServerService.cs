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

        private string _url;
        private string _webDirectory;
        private IDisposable _service;

        public int Order { get; private set; }

        public WebServerService(int order, string url, string webDirectory)
        {
            Order = order;
            _url = url;
            _webDirectory = webDirectory;
        }

        public bool Start()
        {
            Logger.Info("Starting");

            var staticFileOptions = new StaticFileOptions
            {
                FileSystem = new PhysicalFileSystem(_webDirectory)
            };
            _service = WebApp.Start(_url, x => x.UseStaticFiles(staticFileOptions));

            Logger.Info("Started");
            Logger.InfoFormat("Listening on {0}", _url);
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