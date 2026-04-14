using Bugo_shared.Models;
using System.Net.Http.Json;

namespace Bugo_blazor.Services
{
    public class UsuarioService
    {
        private readonly HttpClient _http;

        public UsuarioService(HttpClient http)
        {
            _http = http;
        }

        public async Task<Usuario> GetById(int id)
        {
            return await _http.GetFromJsonAsync<Usuario>($"api/login/{id}");
        }

        public async Task Update(Usuario user)
        {
            await _http.PutAsJsonAsync($"api/usuarios/{user.Id}", user);
        }
    }
}
