using Bugo_shared.Enum;

namespace Bugo_shared.DTOs
{
    public class LoginReponse
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public string Token { get; set; }

        public string Nome { get; set; }
        public string Email { get; set; }
        public PerfilEnum Perfil { get; set; }
    }
}
