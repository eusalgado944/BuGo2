using Bugo_api.Data;
using Bugo_shared.DTOs;
using Bugo_shared.Models;

namespace Bugo_api.Services;

public class UsuarioService
{
    private readonly AppDbContext _context;

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


    public UsuarioService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Usuario?> GetByIdAsync(int id)
        => await _context.Set<Usuario>().FindAsync(id);

    public Usuario? Update(int id, Usuario Atualizado)
    {
        var user = _context.Usuarios.FirstOrDefault(x => x.Id == id);

        if (user == null)
            return null;

        user.Nome = Atualizado.Nome;
        user.Email = Atualizado.Email;
        user.Perfil = Atualizado.Perfil;

        if (!string.IsNullOrEmpty(Atualizado.Senha))
            user.Senha = Atualizado.Senha;

        _context.SaveChanges();

        return user;
    }

    public bool? Delete(int id)
    {
        var user = _context.Usuarios.FirstOrDefault(x => x.Id == id);
        
        if (user == null)
            return false;
        
        _context.Usuarios.Remove(user);
        _context.SaveChanges();
     
        return true;
    }
}