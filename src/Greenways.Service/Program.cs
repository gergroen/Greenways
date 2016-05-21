using Greenways.Config;
using Greenways.Service;
using Topshelf;
using Topshelf.Ninject;

namespace Greenways
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(c =>
            {
                c.UseLog4Net();
                c.UseNinject(new ServiceModule());
                c.Service<CompositeService>(s =>
                {
                    s.ConstructUsingNinject();
                    s.WhenStarted((service, control) => service.Start());
                    s.WhenStopped((service, control) => service.Stop());
                });
            });
        }
    }
}
