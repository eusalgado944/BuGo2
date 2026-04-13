using Bugo_shared.Models;
using Bugo_shared.Enum;
using Bugo_blazor.Data;
using Microsoft.EntityFrameworkCore;

namespace Bugo_blazor.Services
{
    public class ChamadoService
    {
        private readonly AppDbContext _context;

        public ChamadoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Chamado>> GetAll() => await _context.Chamados.ToListAsync();

        public async Task<Chamado?> GetById(int id) => await _context.Chamados.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Chamado> Create(Chamado chamado)
        {
            chamado.DataAbertura = DateTime.UtcNow;
            chamado.Status = StatusChamado.Aberto;

            await _context.Chamados.AddAsync(chamado);
            await _context.SaveChangesAsync();

            return chamado;
        }

        public async Task<Chamado?> AssumirChamado(int id, int tecnicoId)
        {
            var chamado = await GetById(id);

            if (chamado == null) return null;

            chamado.TecnicoId = tecnicoId;
            chamado.Status = StatusChamado.EmAndamento;

            await _context.SaveChangesAsync();

            return chamado;
        }

        public async Task<Chamado?> FinalizarChamado(int id)
        {
            var chamado = await GetById(id);

            if (chamado == null) return null;

            chamado.Status = StatusChamado.Finalizado;

            await _context.SaveChangesAsync();

            return chamado;
        }

        public async Task<Chamado?> Delete(int id)
        {
            var chamado = await GetById(id);

            if (chamado == null) return null;

            _context.Chamados.Remove(chamado);
            await _context.SaveChangesAsync();

            return chamado;
        }

        public async Task Update(Chamado chamado)
        {
            _context.Chamados.Update(chamado);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Chamado>> GetAberto() =>
            await _context.Chamados
                .Where(x => x.Status == StatusChamado.Aberto)
                .ToListAsync();

        public async Task<List<Chamado>> GetPorTecnico(int tecnicoId) =>
            await _context.Chamados
                .Where(x => x.TecnicoId == tecnicoId)
                .ToListAsync();

        public async Task<List<Chamado>> GetPorUsuario(string usuario) =>
            await _context.Chamados
                .Where(x => x.Usuario == usuario)
                .ToListAsync();
    }
}
