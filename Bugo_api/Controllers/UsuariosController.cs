using Bugo_shared.DTOs;
using Bugo_shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Bugo_blazor.Controllers
{

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

        public UsuariosController(Services.UsuarioService service)
        {
            _service = service;
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
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Erro ao buscar usuários", error = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Usuario usuario)
        {
            try
            {
                if (usuario == null)
                    return BadRequest(new { message = "Usuário não pode ser nulo" });

                var novo = _service.Create(usuario);
                return CreatedAtAction(nameof(Get), new { id = novo.Id }, novo);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Erro ao criar usuário", error = ex.Message });
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

                return Ok(new { token, usuario.Id, usuario.Email, usuario.Nome });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro ao fazer login", error = ex.Message });
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
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Erro ao buscar usuário", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Usuario usuario)
        {
            var update = _service.Update(id, usuario);

            if (update != null)
                return NotFound(new { mensage = "Usuário não encontrado" });

            return Ok(update);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var delete = _service.Delete(id);

            if (delete == false)
                return NotFound(new { mensage = "Usuário não encontrado" });

            return NoContent();
        }
    }
    public class LoginRequest
    {
        public string Email { get; set; } = "";
        public string Senha { get; set; } = "";
    }
}
