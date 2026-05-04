using Bugo_shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Bugo_blazor.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly Services.UsuarioService _service;
    private readonly IConfiguration _config;

    public UsuariosController(Services.UsuarioService service, IConfiguration config)
    {
        _service = service;
        _config = config;
    }

    [HttpGet]
    public IActionResult Get()
    {
        try
        {
            var usuarios = _service.GetAll();
            return Ok(usuarios);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro ao buscar usuários", error = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetPorId(int id)
    {
        try
        {
            var usuario = _service.GetAll().FirstOrDefault(u => u.Id == id);
            if (usuario == null)
                return NotFound(new { message = "Usuário não encontrado" });

            return Ok(usuario);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro ao buscar usuário", error = ex.Message });
        }
    }

    [HttpPost]
    public IActionResult Post([FromBody] Usuario usuario)
    {
        try
        {
            if (usuario == null)
                return BadRequest(new { message = "Usuário não pode ser nulo" });

            usuario.Id = 0;
            var novo = _service.Create(usuario);
            return CreatedAtAction(nameof(GetPorId), new { id = novo.Id }, novo);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro ao criar usuário", error = ex.Message });
        }
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        try
        {
            if (request == null || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Senha))
                return BadRequest(new { message = "Email e senha são obrigatórios" });

            var usuario = _service.Login(request.Email, request.Senha);
            if (usuario == null)
                return Unauthorized(new { message = "Email ou senha inválidos" });

            var token = GerarToken(usuario);
            return Ok(new { token, usuario.Id, usuario.Email, usuario.Nome, usuario.Perfil });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro ao fazer login", error = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Usuario usuario)
    {
        try
        {
            var update = _service.Update(id, usuario);

            if (update == null)
                return NotFound(new { message = "Usuário não encontrado" });

            return Ok(update);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro ao atualizar usuário", error = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            var result = _service.Delete(id);

            if (!result)
                return NotFound(new { message = "Usuário não encontrado" });

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro ao deletar usuário", error = ex.Message });
        }
    }

    private string GerarToken(Usuario usuario)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _config["Jwt:Key"] ?? "bugo-secret-key-2024-muito-segura!!"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Email, usuario.Email),
            new Claim(ClaimTypes.Name, usuario.Nome)
        };

        var token = new JwtSecurityToken(
            issuer: "bugo-api",
            audience: "bugo-blazor",
            claims: claims,
            expires: DateTime.UtcNow.AddHours(8),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

public class LoginRequest
{
    public string Email { get; set; } = "";
    public string Senha { get; set; } = "";
}