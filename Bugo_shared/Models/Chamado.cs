using Bugo_shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugo_shared.Models
{
    public class Chamado
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public StatusChamado Status { get; set; }
        public string Usuario { get; set; }
        public string Tecnico { get; set; }
        public DateTime DataAbertura { get; set; }
    }
}
