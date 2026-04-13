using Bugo_shared.Models;
using System.Net.Http.Json;
using Microsoft.JSInterop;
using System.Text.Json;

namespace Bugo_blazor.Services
{
    public class AuthService_old
    {
        private readonly HttpClient _http;
        private readonly IJSRuntime _js;

        public Usuario? UsuarioLogado { get; private set; }
        public string? Token { get; private set; }

        public bool IsAuthenticated => UsuarioLogado != null;

        public AuthService_old(HttpClient http, IJSRuntime js)
        {
            _http = http;
            _js = js;
        }

        public async Task<AuthResult> Login(string email, string senha)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("api/usuarios/login", new
                {
                    email,
                    senha
                });

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    return AuthResult.Fail($"Erro login: {error}");
                }

                var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

                if (result == null || result.Usuario == null)
                    return AuthResult.Fail("Resposta inválida da API");

                UsuarioLogado = result.Usuario;
                Token = result.Token;

                // salva usuario
                await _js.InvokeVoidAsync("localStorage.setItem", "usuario",
                    JsonSerializer.Serialize(UsuarioLogado));

                // salva token
                if (!string.IsNullOrEmpty(Token))
                {
                    await _js.InvokeVoidAsync("localStorage.setItem", "token", Token);
                }

                return AuthResult.Ok(Token);
            }
            catch (HttpRequestException)
            {
                return AuthResult.Fail("API fora do ar");
            }
            catch (Exception ex)
            {
                return AuthResult.Fail($"Erro inesperado: {ex.Message}");
            }
        }

        public async Task LoadUsuario()
        {
            var json = await _js.InvokeAsync<string>("localStorage.getItem", "usuario");
            var token = await _js.InvokeAsync<string>("localStorage.getItem", "token");

            if (!string.IsNullOrEmpty(json))
            {
                UsuarioLogado = JsonSerializer.Deserialize<Usuario>(json);
            }

            if (!string.IsNullOrEmpty(token))
            {
                Token = token;
            }
        }

        public async Task Logout()
        {
            UsuarioLogado = null;
            Token = null;

            await _js.InvokeVoidAsync("localStorage.removeItem", "usuario");
            await _js.InvokeVoidAsync("localStorage.removeItem", "token");
        }
    }

    // ============================
    // Resultado padrão de autenticação
    // ============================
    public class AuthResult
    {
        public bool Success { get; set; }
        public string? Error { get; set; }
        public string? Token { get; set; }

        public static AuthResult Ok(string? token = null)
            => new() { Success = true, Token = token };

        public static AuthResult Fail(string error)
            => new() { Success = false, Error = error };
    }

    // ============================
    // Modelo esperado da API
    // ============================
    public class LoginResponse
    {
        public Usuario? Usuario { get; set; }
        public string? Token { get; set; }
    }
}