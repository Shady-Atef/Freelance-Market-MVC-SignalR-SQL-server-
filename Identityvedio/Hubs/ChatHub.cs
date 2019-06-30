#region MyRegion
//using Identityvedio.Models;
//using Microsoft.AspNet.SignalR;
//using Microsoft.AspNet.SignalR.Hubs;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using Microsoft.AspNet.Identity;


//namespace Identityvedio.Hubs
//{
//    [HubName("ChatHub")]
//    public class ChatHub : Hub
//    {
//        private ApplicationDbContext db = new ApplicationDbContext();

//        [HubMethodName("Message")]
//        public void Message(string msgText, int ProposalId)
//        {

//            //Groups.Add(Context.ConnectionId, ProposalId.ToString());

//            //Message msg = new Message();
//            //msg.FreelanceId = HttpContext.Current.User.Identity.GetUserId();
//            //msg.ManagerId = "2";
//            //msg.MessageText = msgText;
//            //msg.MessageTime = DateTime.Now;
//            //msg.ProposalId = ProposalId;
//            //db.Messages.Add(msg);
//            //db.SaveChanges();
//            ////Clients.All.Comment(msgText);
//            //Clients.Group(ProposalId.ToString()).Comment(msgText); 




//            if (Context.User.Identity.GetUserId() == db.Proposals.Where(p=>p.ID==ProposalId).Select(p=>p.FreelanceId).FirstOrDefault())
//            {
//                var userid = db.Proposals.Where(p => p.ID == ProposalId).Select(p => p.Job.ClientId).FirstOrDefault();

//                Message msg = new Message();
//                msg.FreelanceId = HttpContext.Current.User.Identity.GetUserId();
//                msg.ManagerId = userid;
//                msg.MessageText = msgText;
//                msg.MessageTime = DateTime.Now;
//                msg.ProposalId = ProposalId;
//                db.Messages.Add(msg);
//                db.SaveChanges();
//                Clients.All.Comment(msgText);
//            }
//            if (Context.User.Identity.GetUserId() == db.Proposals.Where(p => p.ID == ProposalId).Select(p => p.Job.ClientId).FirstOrDefault())
//            {
//                var userid = db.Proposals.Where(p => p.ID == ProposalId).Select(p => p.FreelanceId).FirstOrDefault();

//                Message msg = new Message();
//                msg.FreelanceId = userid;
//                msg.ManagerId = HttpContext.Current.User.Identity.GetUserId();
//                msg.MessageText = msgText;
//                msg.MessageTime = DateTime.Now;
//                msg.ProposalId = ProposalId;
//                db.Messages.Add(msg);
//                db.SaveChanges();
//                Clients.All.Comment(msgText);
//                //Clients.User(userid).Comment(msgText);
//                //Clients.User(Context.User.Identity.GetUserId()).Comment(msgText);

//            }





//        }
//    }
//}
#endregion


using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Identityvedio.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Identityvedio.Hubs
{
    [Authorize]
    [HubName("ChatHub")]
    public class ChatHub : Hub
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private readonly static ConnectionMapping<string> _connections =
            new ConnectionMapping<string>();

        [HubMethodName("Message")]
        public void Message(string msgText, int ProposalId)
        {
            //string name = Context.User.Identity.Name;

            if (Context.User.Identity.GetUserId() == db.Proposals.Where(p => p.ID == ProposalId).Select(p => p.FreelancerId).FirstOrDefault())
            {
                var userId = db.Proposals.Where(p => p.ID == ProposalId).Select(p => p.Job.ClientId).FirstOrDefault();

                Message msg = new Message();
                msg.FreelanceId = HttpContext.Current.User.Identity.GetUserId();
                msg.ManagerId = userId;
                msg.Sender = HttpContext.Current.User.Identity.GetUserId();
                msg.MessageText = msgText;
                msg.MessageTime = DateTime.Now;
                msg.ProposalId = ProposalId;
                db.Messages.Add(msg);
                db.SaveChanges();
                foreach (var connectionId in _connections.GetConnections(userId))
                {
                    Clients.Client(connectionId).Comment(msgText);
                }
            }
            if (Context.User.Identity.GetUserId() == db.Proposals.Where(p => p.ID == ProposalId).Select(p => p.Job.ClientId).FirstOrDefault())
            {
                var userId = db.Proposals.Where(p => p.ID == ProposalId).Select(p => p.FreelancerId).FirstOrDefault();

                Message msg = new Message();
                msg.FreelanceId = userId;
                msg.ManagerId = HttpContext.Current.User.Identity.GetUserId();
                msg.Sender = HttpContext.Current.User.Identity.GetUserId();
                msg.MessageText = msgText;
                msg.MessageTime = DateTime.Now;
                msg.ProposalId = ProposalId;
                db.Messages.Add(msg);
                db.SaveChanges();
                foreach (var connectionId in _connections.GetConnections(userId))
                {
                    Clients.Client(connectionId).Comment(msgText);
                }
            }

        }

        public override Task OnConnected()
        {
            string userId = Context.User.Identity.GetUserId();


            _connections.Add(userId, Context.ConnectionId);

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            string userId = Context.User.Identity.GetUserId();

            _connections.Remove(userId, Context.ConnectionId);

            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            string userId = Context.User.Identity.GetUserId();

            if (!_connections.GetConnections(userId).Contains(Context.ConnectionId))
            {
                _connections.Add(userId, Context.ConnectionId);
            }

            return base.OnReconnected();
        }
    }
}