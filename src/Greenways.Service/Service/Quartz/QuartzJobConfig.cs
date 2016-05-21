using Quartz;

namespace Greenways.Service.Quartz
{
    public interface IQuartzJobConfig
    {
        IJobDetail JobDetail { get; }
        ITrigger Trigger { get; }
    }

    public class QuartzJobConfig<TJob> : IQuartzJobConfig where TJob : IJob
    {
        public QuartzJobConfig(QuartzTrigger<TJob> quartzTrigger)
        {
            JobDetail = JobBuilder.Create<TJob>().Build();
            Trigger = quartzTrigger.Trigger;
        }

        public IJobDetail JobDetail { get; private set; }
        public ITrigger Trigger { get; private set; }
    }

    public class QuartzTrigger<TJob> where TJob : IJob
    {
        public QuartzTrigger(ITrigger trigger)
        {
            Trigger = trigger;
        }

        public ITrigger Trigger { get; private set; }
    }
}