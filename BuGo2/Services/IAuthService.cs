using Bugo_shared.Models;

namespace Bugo_blazor.Services
{
    public interface IAuthService
    {
        Usuario? UsuarioLogado { get; }
        string? Token { get; }
        bool IsAuthenticated { get; }

        Task<AuthResult> Login(string email, string senha);
        Task LoadUsuario();
        Task Logout();
    }
}