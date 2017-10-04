using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Collections.Generic;

namespace GIFU.Hubs
{
    [HubName("messagesHub")]
    public class MessagesHub : Hub
    {
        public static Dictionary<string, ClientInfo> currentClients = new Dictionary<string, ClientInfo>();

        public MessagesHub()
        {
            MessagesRepository.GetInstance();
        }

        [HubMethodName("sendMessages")]
        public static void SendMessages()
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<MessagesHub>();
            context.Clients.All.updateMessages();
        }

        ////連線建立時觸發
        //public override Task OnDisconnected(bool stopCalled)
        //{
        //    if (stopCalled)
        //    {
        //        string cid = Context.ConnectionId;
        //        lock (currentClients)
        //        {
        //            if (currentClients.ContainsKey(cid))
        //            {
        //                currentClients.Remove(cid);
        //                //Todo: 連線終止
        //            }
        //        }
        //    }
        //    return base.OnDisconnected(stopCalled);
        //}

        ////連線建立時觸發
        //public override Task OnConnected()
        //{
        //    string cid = Context.ConnectionId;
        //    return null;
        //}
    }
}