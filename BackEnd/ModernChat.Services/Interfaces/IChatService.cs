using System.Collections.Generic;
using System.Threading.Tasks;
using ModernChat.Models.InputModels.Chat;
using ModernChat.Models.ViewModels.Chat;

namespace ModernChat.Services.Interfaces
{
    public interface IChatService
    {
        Task<List<ChatViewModel>> GetChats(bool userChatsOnly, int currentUserId);
        Task<List<int>> GetChatIds(bool userChatsOnly, int currentUserId);
        Task Create(ChatInputModel inputModel, int currentUserId);
    }
}
