using Bugo_shared.Enum;
using System.Text.Json.Serialization;

namespace Bugo_shared.DTOs
{
    public class LoginReponse
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("nome")]
        public string Password { get; set; }

        public PerfilEnum Perfil { get; set; }
    }
}
