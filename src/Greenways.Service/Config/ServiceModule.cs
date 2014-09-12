using Greenways.Service;
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
            Bind<CompositeService>().ToSelf();

            Bind<Microsoft.AspNet.SignalR.IDependencyResolver>().To<SignalRNinjectDependencyResolver>();
            Bind<System.Web.Http.Dependencies.IDependencyResolver>().To<WebApiNinjectDependencyResolver>();
            Bind<IJobFactory>().To<QuartzNinjectJobFactory>();
            Bind<ISchedulerFactory>().To<QuartzNinjectSchedulerFactory>();

            Bind<IService>().To<WebApiService>()
                .WithPropertyValue(x => x.Order, 1)
                .WithPropertyValue(x => x.Url, "http://*/api/");
            Bind<IService>().To<SignalRService>()
                .WithPropertyValue(x => x.Order, 1)
                .WithPropertyValue(x => x.Url, "http://*:8080/signalr/");
            Bind<IService>().To<QuartzService>()
                .WithPropertyValue(x => x.Order, 2);
            Bind<IService>().To<WebServerService>()
                .WithPropertyValue(x => x.Order, 3)
                .WithPropertyValue(x => x.Url, "http://*/www/")
                .WithPropertyValue(x => x.WebDirectory, @"C:\Data\Development\Source\Microsoft.Net\Greenways\src\Greenways.App\");

            Bind<IQuartzJobConfig>().To<QuartzJobConfig<TestJob>>();
            var trigger = TriggerBuilder.Create()
                .StartNow().WithCronSchedule("* * * * * ?")
                .Build();
            Bind<QuartzTrigger<TestJob>>().ToSelf()
                .WithConstructorArgument("trigger", trigger);
        }
    }
}