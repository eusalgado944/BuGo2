using Bugo_shared.Models;
using System.Net.Http.Json;

namespace Bugo_blazor.Services
{
    public class ChamadoService
    {
        private readonly HttpClient _http;

        public ChamadoService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Chamado>> GetAbertos()
        {
            return await _http.GetFromJsonAsync<List<Chamado>>("api/chamados/abertos");
        }

        public async Task<List<Chamado>> GetPorTecnico(int tecnicoId)
        {
            return await _http.GetFromJsonAsync<List<Chamado>>($"api/chamados/tecnico/{tecnicoId}");
        }

        public async Task Assumir(int id, int tecnicoId)
        {
            await _http.PutAsJsonAsync($"api/chamados/{id}/assumir", tecnicoId);
        }

        public async Task Finalizar(int id)
        {
            await _http.PutAsync($"api/chamados/{id}/finalizar", null);
        }
    }
}
