using Bugo_shared.Enum;
using Bugo_shared.Models;

namespace Bugo_blazor.Services
{
    public interface IAuthService
    {
        Usuario? UsuarioLogado { get; }
        string? Token { get; }
        bool IsAuthenticated { get; }
        PerfilEnum? Perfil { get; }

        Task<AuthResult> Login(string email, string senha);
        Task LoadUsuario();
        Task Logout();
    }
}