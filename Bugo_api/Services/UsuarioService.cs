using Bugo_shared.Models;
using Bugo_api.Data;

namespace Bugo_api.Services;

public class UsuarioService
{
    private readonly AppDbContext _context;

    public UsuarioService(AppDbContext context)
    {
        _context = context;
    }

    public List<Usuario> GetAll()
    {
        return _context.Usuarios.ToList();
    }

    public Usuario Create(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        _context.SaveChanges();
        return usuario;
    }

    public Usuario? Login(string email, string senha)
    {
        return _context.Usuarios
            .FirstOrDefault(x => x.Email == email && x.Senha == senha);
    }
}