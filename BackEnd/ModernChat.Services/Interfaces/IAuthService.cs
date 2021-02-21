using ModernChat.Models.InputModels.Auth;
using System.Threading.Tasks;

namespace ModernChat.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> Register(RegisterInputModel inputModel);
        Task<string> Login(LoginInputModel inputModel);
    }
}
