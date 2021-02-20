using ModernChat.Models.InputModels.Auth;
using System.Threading.Tasks;

namespace ModernChat.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> Register(RegisterIM inputModel);
        Task<string> Login(LoginIM inputModel);
    }
}
