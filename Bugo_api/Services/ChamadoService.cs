using Bugo_shared.Models;
using Bugo_shared.Enum;
using Bugo_api.Data;

namespace Bugo_api.Services
{
    public class ChamadoService
    {
        private readonly AppDbContext _context;

        public ChamadoService(AppDbContext context)
        {
            _context = context;
        }

        public List<Chamado> GetAll() => _context.Chamados.ToList();

        public Chamado Create(Chamado chamado)
        {
            chamado.DataAbertura = DateTime.UtcNow;
            chamado.Status = StatusChamado.Aberto;

            _context.Chamados.Add(chamado);
            _context.SaveChanges();

            return chamado;
        }

        public Chamado? AssumirChamado(int id, int tecnicoId)
        {
            var chamado = _context.Chamados.FirstOrDefault(x => x.Id == id);

            if (chamado == null) return null;

            chamado.tecnicoId = tecnicoId;
            chamado.Status = StatusChamado.EmAndamento;

            _context.SaveChanges();

            return chamado;
        }

        public Chamado? FinalizarChamado(int id)
        {
            var chamado = _context.Chamados.FirstOrDefault(x => x.Id == id);

            if (chamado == null) return null;

            chamado.Status = StatusChamado.Finalizado;

            _context.SaveChanges();

            return chamado;
        }

        public Chamado? Delete(int id)
        {
            var chamado = _context.Chamados.FirstOrDefault(x => x.Id == id);

            if (chamado == null) return null;

            _context.Chamados.Remove(chamado);
            _context.SaveChanges();

            return chamado;
        }

        public void Update(Chamado chamado)
        {
            _context.Chamados.Update(chamado);
            _context.SaveChanges();
        }

        public List<Chamado> GetAberto()
        {
            return _context.Chamados
                .Where(x => x.Status == StatusChamado.Aberto)
                .ToList();
        }

        public List<Chamado> GetPorTecnico(int tecnicoId)
        {
            return _context.Chamados
                .Where(x => x.tecnicoId == tecnicoId)
                .ToList();
        }

        public List<Chamado> GetPorUsuario(string usuario)
        {
            return _context.Chamados
                .Where(x => x.Usuario == usuario)
                .ToList();
        }
    }
}
