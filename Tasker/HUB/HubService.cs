using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;

namespace Tasker.HUB
{
    public class HubService : Hub<IHubService>
    {
        public async Task SendMessage(string user, string message)
            => await Clients.All.ReceiveMessage(user, message);

        public async Task SendMessageToCaller(string user, string message)
            => await Clients.Caller.ReceiveMessage(user, message);

        public async Task SendMessageToGroup(string user, string message)
            => await Clients.Group("SignalR Users").ReceiveMessage(user, message);
    }
    public interface IHubService
    {
        Task ReceiveMessage(string user, string message);
    }
}
