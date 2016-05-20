using System;
using Common.Logging;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;
using Greenways.Service.Owin;

namespace Greenways.Service.WebServer
{
    public class WebServerService : IOwinService
    {
        private static readonly ILog Logger = LogManager.GetCurrentClassLogger();

        private IDisposable _service;

        public int Order { get; set; }
        public string WebDirectory { get; set; }

        public void Configuration(IAppBuilder appBuilder)
        {
            Logger.Info("Configuring");

            var physicalFileSystem = new PhysicalFileSystem(WebDirectory);

            var staticFileOptions = new StaticFileOptions
            {
                FileSystem = physicalFileSystem,
                ServeUnknownFileTypes = true
            };
            appBuilder.UseStaticFiles(staticFileOptions);

            //var options = new DefaultFilesOptions();
            //options.FileSystem = physicalFileSystem;
            //appBuilder.UseDefaultFiles(options);

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