using ModernChat.Models.ViewModels.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ModernChat.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserViewModel>> GetUsers(int currentUserId);
    }
}
