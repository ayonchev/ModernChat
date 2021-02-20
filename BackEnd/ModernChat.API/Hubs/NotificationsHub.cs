using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using ModernChat.API.Hubs.Clients;
using ModernChat.Models.InputModels.Message;
using ModernChat.Services;
using ModernChat.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ModernChat.API.Hubs
{
    [Authorize]
    public partial class NotificationsHub : Hub<INotificationsClient>
    {
        public const string Path = "/notifications";

        private readonly IMessageService messageService;
        private readonly IChatService chatService;
        private readonly HubStoreService store;

        public NotificationsHub(
            IMessageService messageService,
            IChatService chatService,
            HubStoreService store)
        {
            this.messageService = messageService;
            this.chatService = chatService;
            this.store = store;
        }

        public override async Task OnConnectedAsync()
        {
            var userId = int.Parse(Context.UserIdentifier);
            var chatIds = await chatService.GetChatIds(true, userId);

            foreach (var id in chatIds)
            {
                var groupName = id.ToString();

                if (!store.ChatGroupIds.ContainsKey(id))
                {
                    store.ChatGroupIds.Add(id, groupName);
                }

                await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            }

            var currentActiveUsers = store
                .ActiveUserConnections
                .Select(au => au.Key.ToString())
                .Where(id => id != userId.ToString())
                .ToList();
            var username = GetUsername();

            await Clients.Users(currentActiveUsers).Connect(username);

            if (!store.ActiveUserConnections.ContainsKey(userId))
            {
                store.ActiveUserConnections.Add(userId, 0);
            }

            store.ActiveUserConnections[userId]++;

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userId = int.Parse(Context.UserIdentifier);

            store.ActiveUserConnections[userId]--;

            if(store.ActiveUserConnections[userId] == 0)
            {
                var username = GetUsername();
                await Clients.All.Disconnect(username);
            }

            await base.OnDisconnectedAsync(exception);
        }

        private string GetUsername()
        {
            var username = Context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;

            return username;
        }
    }
}
