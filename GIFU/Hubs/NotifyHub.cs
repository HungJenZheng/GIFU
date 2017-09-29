using Microsoft.AspNet.SignalR;

namespace GIFU.Hubs
{
    public class NotifyHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }

        public void Send(string name, string message)
        {

        }
    }
}