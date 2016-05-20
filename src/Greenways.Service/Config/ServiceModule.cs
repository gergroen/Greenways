using Greenways.Service;
using Greenways.Service.Owin;
using Greenways.Service.Quartz;
using Greenways.Service.Quartz.Jobs;
using Greenways.Service.Quartz.Ninject;
using Greenways.Service.SignalR;
using Greenways.Service.SignalR.Ninject;
using Greenways.Service.WebApi;
using Greenways.Service.WebApi.Ninject;
using Greenways.Service.WebServer;
using Greenways.Utils.Ninject;
using Ninject.Modules;
using Quartz;
using Quartz.Spi;

namespace Greenways.Config
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            var appSettings = System.Configuration.ConfigurationManager.AppSettings;

            Bind<CompositeService>().ToSelf();

            Bind<Microsoft.AspNet.SignalR.IDependencyResolver>().To<SignalRNinjectDependencyResolver>();
            Bind<System.Web.Http.Dependencies.IDependencyResolver>().To<WebApiNinjectDependencyResolver>();

            Bind<IService>().To<OwinHostService>()
                .WithPropertyValue(x => x.Order, 1)
                .WithPropertyValue(x => x.Url, appSettings["Url"]);

            Bind<IOwinService>().To<SignalRService>()
                .WithPropertyValue(x => x.Order, 1);
            Bind<IOwinService>().To<WebApiService>()
                .WithPropertyValue(x => x.Order, 2);
            Bind<IOwinService>().To<WebServerService>()
                .WithPropertyValue(x => x.Order, 3)
                .WithPropertyValue(x => x.WebDirectory, appSettings["WebServerDirectory"]);

            Bind<IJobFactory>().To<QuartzNinjectJobFactory>();
            Bind<ISchedulerFactory>().To<QuartzNinjectSchedulerFactory>();

            Bind<IService>().To<QuartzService>()
                .WithPropertyValue(x => x.Order, 2);

            Bind<IQuartzJobConfig>().To<QuartzJobConfig<TestJob>>();
            var trigger = TriggerBuilder.Create()
                .StartNow().WithCronSchedule("0/5 * * * * ?")
                .Build();
            Bind<QuartzTrigger<TestJob>>().ToSelf()
                .WithConstructorArgument("trigger", trigger);
        }
    }
}