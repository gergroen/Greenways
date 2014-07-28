using System;
using System.Globalization;
using Common.Logging;
using Ninject;
using Quartz;
using Quartz.Spi;

namespace Greenways.Service.Quartz.Ninject
{
    public class QuartzNinjectJobFactory : IJobFactory
    {
        private readonly IKernel _kernel;

        private static readonly ILog Logger = LogManager.GetLogger(typeof(QuartzNinjectJobFactory));

        public QuartzNinjectJobFactory(IKernel kernel)
        {
            _kernel = kernel;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            IJobDetail jobDetail = bundle.JobDetail;
            Type jobType = jobDetail.JobType;
            try
            {
                if (Logger.IsDebugEnabled)
                {
                    Logger.Debug(string.Format(CultureInfo.InvariantCulture, "Producing instance of Job '{0}', class={1}", jobDetail.Key, jobType.FullName));
                }

                return _kernel.Get(jobType) as IJob;
            }
            catch (Exception e)
            {
                var se = new SchedulerException(string.Format(CultureInfo.InvariantCulture, "Problem instantiating class '{0}'", jobDetail.JobType.FullName), e);
                throw se;
            }
        }

        public void ReturnJob(IJob job)
        {

        }
    }
}