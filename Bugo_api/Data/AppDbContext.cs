using Bugo_shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Bugo_api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Chamado> Chamados { get; set; }
    }
}
