using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugo_shared.DTOs
{
    public class UsuarioUpdateDto
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Perfil { get; set; }

        //teste pra reset de senha
        public string Senha { get; set; }
    }
}
