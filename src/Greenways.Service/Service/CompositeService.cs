using System.Collections.Generic;
using System.Linq;
using Common.Logging;

namespace Greenways.Service
{
    public class CompositeService
    {
        private static readonly ILog Logger = LogManager.GetCurrentClassLogger();
        private readonly IList<IService> _services;

        public CompositeService(IList<IService> services)
        {
            _services = services;
        }

        public bool Start()
        {
            Logger.Info("Starting");
            var result =  _services.OrderBy(x => x.Order).All(x => x.Start());
            Logger.Info("Started");
            return result;
        }

        public bool Stop()
        {
            Logger.Info("Stopping");
            var result = _services.OrderByDescending(x => x.Order).All(x => x.Stop());
            Logger.Info("Stopped");
            return result;
        }
    }
}