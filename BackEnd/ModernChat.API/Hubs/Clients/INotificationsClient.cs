using System.Threading.Tasks;
using ModernChat.Models.ViewModels.Message;

namespace ModernChat.API.Hubs.Clients
{
    public interface INotificationsClient
    {
        Task Connect(string username);
        Task Disconnect(string username);
        Task ReceiveMessage(MessageViewModel message);
        Task DeleteMessage(int messageId);
    }
}
