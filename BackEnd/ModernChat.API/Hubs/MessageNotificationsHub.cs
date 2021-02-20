using Microsoft.AspNetCore.SignalR;
using ModernChat.API.Hubs.Clients;
using ModernChat.Models.InputModels.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModernChat.API.Hubs
{
    public partial class NotificationsHub : Hub<INotificationsClient>
    {
        public async Task SendMessage(MessageInputModel inputModel)
        {
            var message = await messageService.Create(inputModel);
            var groupName = store.ChatGroupIds[inputModel.ChatId];

            await Clients.Group(groupName).ReceiveMessage(message);
        }

        public async Task DeleteMessage(int messageId, int chatId)
        {
            await messageService.Delete(messageId);
            var groupName = store.ChatGroupIds[chatId];

            await Clients.Group(groupName).DeleteMessage(messageId);
        }
    }
}
