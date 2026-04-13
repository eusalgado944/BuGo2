using Bugo_shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Bugo_blazor.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Chamado> Chamados { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
