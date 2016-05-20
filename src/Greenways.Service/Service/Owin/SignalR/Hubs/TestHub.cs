using Microsoft.AspNet.SignalR;

namespace Greenways.Service.SignalR.Hubs
{
    public class TestHub : Hub
    {
        public TestHub()
        {
        }

        public void Send(string name)
        {
            Clients.All.SendToClients(name);
        }
    }
}
