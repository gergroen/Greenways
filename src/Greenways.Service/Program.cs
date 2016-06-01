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
                //c.UseLog4Net();
                c.UseNinject(new ServiceModule());
                c.RunAsLocalSystem();                           
                c.SetDisplayName("Greenways"); 
                c.SetServiceName("Greenways");
                c.SetDescription("Greenways");
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
