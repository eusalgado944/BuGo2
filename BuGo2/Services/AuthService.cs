using Bugo_shared.Models;
using System.Net.Http.Json;
using Microsoft.JSInterop;

namespace Bugo_blazor.Services
{
    public class AuthService
    {
        private readonly HttpClient _http;
        private readonly IJSRuntime _js;

        public Usuario? UsuarioLogado { get; private set; }

        public AuthService(HttpClient http, IJSRuntime js)
        {
            _http = http;
            _js = js;
        }

        public async Task<bool> Login(string email, string senha)
        {
            var response = await _http.PostAsJsonAsync("api/usuarios/login", new
            {
                email,
                senha
            });

            if (response.IsSuccessStatusCode)
                return false;

            UsuarioLogado = await response.Content.ReadFromJsonAsync<Usuario>();

            await _js.InvokeVoidAsync("localStorage.setItem", "usuario",
                System.Text.Json.JsonSerializer.Serialize(UsuarioLogado));

            return true;
        }

        public async Task LoadUsuario()
        {
            var json = await _js.InvokeAsync<string>("localStorage.getItem", "usuario");

            if (!string.IsNullOrEmpty(json))
            {
                UsuarioLogado = System.Text.Json.JsonSerializer.Deserialize<Usuario>(json);
            }
        }

        public async Task Logout()
        {
            UsuarioLogado = null;
            await _js.InvokeVoidAsync("localStorage.removeItem", "usuario");
        }
    }
}
