using Bugo_shared.Enum;
using System;
using System.Net.Mail;

namespace Bugo_shared.Models
{
    public class Chamado
    {
        public int Id { get; set; }
        public int? TecnicoId { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public StatusChamado Status { get; set; }
        public PrioridadeEnum Prioridade { get; set; } = PrioridadeEnum.Baixa;
        public string Usuario { get; set; } = string.Empty;
        public DateTime DataAbertura { get; set; }
    }
}
