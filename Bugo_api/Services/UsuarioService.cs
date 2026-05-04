using Bugo_blazor.Data;
using Bugo_shared.Models;

namespace Bugo_blazor.Services;

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
        usuario.Id = 0; // garante que o banco gera o Id
        _context.Usuarios.Add(usuario);
        _context.SaveChanges();
        return usuario;
    }

    public Usuario? Login(string email, string senha)
    {
        return _context.Usuarios
            .FirstOrDefault(x => x.Email == email && x.Senha == senha);
    }

    public async Task<Usuario?> GetByIdAsync(int id)
        => await _context.Set<Usuario>().FindAsync(id);

    public Usuario? Update(int id, Usuario atualizado)
    {
        var user = _context.Usuarios.FirstOrDefault(x => x.Id == id);

        if (user == null)
            return null;

        user.Nome = atualizado.Nome;
        user.Email = atualizado.Email;
        user.Perfil = atualizado.Perfil;

        if (!string.IsNullOrEmpty(atualizado.Senha))
            user.Senha = atualizado.Senha;

        _context.SaveChanges();
        return user;
    }

    public bool Delete(int id)
    {
        var user = _context.Usuarios.FirstOrDefault(x => x.Id == id);

        if (user == null)
            return false;

        _context.Usuarios.Remove(user);
        _context.SaveChanges();
        return true;
    }
}