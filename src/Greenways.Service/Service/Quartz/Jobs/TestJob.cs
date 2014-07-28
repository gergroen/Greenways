using Common.Logging;
using Microsoft.AspNet.SignalR.Client;
using Quartz;

namespace Greenways.Service.Quartz.Jobs
{
    public class TestJob : IJob
    {
        private static readonly ILog Logger = LogManager.GetCurrentClassLogger();

        public void Execute(IJobExecutionContext context)
        {
            Logger.Info("Executing");

            var hubConnection = new HubConnection("http://localhost:8080");
            var testHubProxy = hubConnection.CreateHubProxy("TestHub");

            hubConnection.Start().Wait();
            testHubProxy.Invoke("Send", "Tick");
            hubConnection.Stop();
            hubConnection.Dispose();

            Logger.Info("Executed");
        }
    }
}
