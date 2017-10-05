using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        /// <summary>
        /// 呼叫用戶端更新資料
        /// </summary>
        /// <param name="cid"></param>
        public static void SendMessages(string cid)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<MessagesHub>();
            if (cid == "*")
            {
                context.Clients.All.updateMessages();
            }
            else
            {
                context.Clients.Client(cid).updateMessages();
            }
        }

        //連線建立時觸發
        public override Task OnDisconnected(bool stopCalled)
        {
            if (stopCalled)
            {
                string cid = Context.ConnectionId;
                lock (currentClients)
                {
                    if (currentClients.ContainsKey(cid))
                    {
                        currentClients.Remove(cid);
                        //Todo: 連線終止
                    }
                }
            }
            return base.OnDisconnected(stopCalled);
        }

        //連線建立時觸發
        public override Task OnConnected()
        {
            string cid = Context.ConnectionId;
            return null;
        }

        public void Register(int? userId)
        {
            if (userId == null) return;
            string cid = Context.ConnectionId;
            lock (currentClients)
            {
                if (!currentClients.ContainsKey(cid))
                {
                    currentClients.Add(cid, new ClientInfo()
                    {
                        ConnectionId = cid,
                        UserId = userId
                    });
                }
            }
        }
    }
}