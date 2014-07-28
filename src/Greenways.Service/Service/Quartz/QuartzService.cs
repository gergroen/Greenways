using System.Collections.Generic;
using Common.Logging;
using Greenways.Service.Quartz.Jobs;
using Quartz;

namespace Greenways.Service.Quartz
{
    public class QuartzService : IService
    {
        private static readonly ILog Logger = LogManager.GetCurrentClassLogger();
        private readonly IScheduler _scheduler;

        public QuartzService(int order, ISchedulerFactory schedulerFactory, IList<IQuartzJobConfig> jobConfigs)
        {
            Order = order;
            _scheduler = schedulerFactory.GetScheduler();

            foreach (var jobConfig in jobConfigs)
            {
                _scheduler.ScheduleJob(jobConfig.JobDetail, jobConfig.Trigger); 
            }
        }

        public int Order { get; private set; }

        public bool Start()
        {
            Logger.Info("Starting");

            _scheduler.Start();

            Logger.Info("Started");
            return true;
        }

        public bool Stop()
        {
            Logger.Info("Stopping");

            _scheduler.Shutdown(true);

            Logger.Info("Stopped");
            return true;
        }
    }
}
