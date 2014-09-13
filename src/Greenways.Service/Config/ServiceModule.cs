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
            var appSettings = System.Configuration.ConfigurationManager.AppSettings;

            Bind<CompositeService>().ToSelf();

            Bind<Microsoft.AspNet.SignalR.IDependencyResolver>().To<SignalRNinjectDependencyResolver>();
            Bind<System.Web.Http.Dependencies.IDependencyResolver>().To<WebApiNinjectDependencyResolver>();
            Bind<IJobFactory>().To<QuartzNinjectJobFactory>();
            Bind<ISchedulerFactory>().To<QuartzNinjectSchedulerFactory>();

            Bind<IService>().To<WebApiService>()
                .WithPropertyValue(x => x.Order, 1)
                .WithPropertyValue(x => x.Url, appSettings["WebApiUrl"]);
            Bind<IService>().To<SignalRService>()
                .WithPropertyValue(x => x.Order, 1)
                .WithPropertyValue(x => x.Url, appSettings["SignalRUrl"]);
            Bind<IService>().To<QuartzService>()
                .WithPropertyValue(x => x.Order, 2);
            Bind<IService>().To<WebServerService>()
                .WithPropertyValue(x => x.Order, 3)
                .WithPropertyValue(x => x.Url, appSettings["WebServerUrl"])
                .WithPropertyValue(x => x.WebDirectory, appSettings["WebServerDirectory"]);

            Bind<IQuartzJobConfig>().To<QuartzJobConfig<TestJob>>();
            var trigger = TriggerBuilder.Create()
                .StartNow().WithCronSchedule("* * * * * ?")
                .Build();
            Bind<QuartzTrigger<TestJob>>().ToSelf()
                .WithConstructorArgument("trigger", trigger);
        }
    }
}