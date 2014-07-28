using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace Greenways.Service.Quartz.Ninject
{
    public class QuartzNinjectSchedulerFactory : StdSchedulerFactory
    {
        private readonly IJobFactory _ninjectJobFactory;

        public QuartzNinjectSchedulerFactory(IJobFactory ninjectJobFactory)
        {
            _ninjectJobFactory = ninjectJobFactory;
        }

        protected override IScheduler Instantiate(global::Quartz.Core.QuartzSchedulerResources rsrcs, global::Quartz.Core.QuartzScheduler qs)
        {
            qs.JobFactory = _ninjectJobFactory;
            return base.Instantiate(rsrcs, qs);
        }
    }
}
