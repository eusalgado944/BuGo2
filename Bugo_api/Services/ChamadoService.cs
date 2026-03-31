using Bugo_shared.Models;
using Bugo_shared.Enum;
using System;
using System.Collections.Generic;

namespace Bugo_api.Services
{
    public class ChamadoService
    {
        private static List<Chamado> chamados = new();

        public List<Chamado> GetAll() => chamados;

        public Chamado Create(Chamado chamado)
        {
            chamado.Id = chamados.Count + 1;
            chamado.DataAbertura = DateTime.Now;
            chamado.Status = Bugo_shared.Enum.StatusChamado.Aberto;

            chamados.Add(chamado);

            return chamado;
        }

        public Chamado? AssumirChamado(int id, string Tecnico)
        {
            var chamado = chamados.FirstOrDefault(x => x.Id == id);

            if (chamado == null) return null;

            chamado.Tecnico = Tecnico;
            chamado.Status = StatusChamado.EmAndamento;

            return chamado;
        }

        public Chamado? FinalizarChamado(int id)
        {
            var chamado = chamados.FirstOrDefault(x => x.Id == id);

            if (chamado == null) return null;

            chamado.Status = StatusChamado.Finalizado;

            return chamado;
        }
    }
}
