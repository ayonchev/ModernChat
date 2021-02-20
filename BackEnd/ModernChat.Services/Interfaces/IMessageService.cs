using System.Collections.Generic;
using System.Threading.Tasks;
using ModernChat.Models.InputModels.Message;
using ModernChat.Models.ViewModels.Message;

namespace ModernChat.Services.Interfaces
{
    public interface IMessageService
    {
        Task<List<MessageViewModel>> Get(int chatId);
        Task<MessageViewModel> Create(MessageInputModel inputModel);
        Task Delete(int messageId);
    }
}
