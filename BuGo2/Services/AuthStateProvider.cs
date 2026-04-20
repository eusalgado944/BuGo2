using Bugo_shared.Enum;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Bugo_blazor.Services
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly IAuthService _authService;

        public AuthStateProvider(IAuthService authService)
        {
            _authService = authService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            await _authService.LoadUsuario();

            if (!_authService.IsAuthenticated || _authService.UsuarioLogado == null)
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, _authService.UsuarioLogado.Id.ToString()),
                new Claim(ClaimTypes.Name, _authService.UsuarioLogado.Nome),
                new Claim(ClaimTypes.Email, _authService.UsuarioLogado.Email),
                new Claim(ClaimTypes.Role, _authService.UsuarioLogado.Perfil.ToString())
            };

            var identity = new ClaimsIdentity(claims, "BuGoAuth");
            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        public void NotificarLogin()
        {
            var user = _authService.UsuarioLogado;
            if (user == null) return;

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Nome),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Perfil.ToString())
            };

            var identity = new ClaimsIdentity(claims, "BuGoAuth");
            var principal = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
        }

        public void NotificarLogout()
        {
            var anonimo = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonimo)));
        }
    }
}