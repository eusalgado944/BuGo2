using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Bugo_shared.Models;

namespace Bugo_api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly Services.UsuarioService _service;

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
        public IActionResult Login([FromBody] Usuario user)
        {
            try
            {
                if (user == null || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Senha))
                    return BadRequest(new { message = "Email e senha são obrigatórios" });

                var usuario = _service.Login(user.Email, user.Senha);
                if (usuario == null)
                    return Unauthorized(new { message = "Email ou senha inválidos" });

                var usuarioResponse = new
                {
                    usuario.Id,
                    usuario.Email,
                    usuario.Nome
                };

                return Ok(usuarioResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Erro ao fazer login", error = ex.Message });
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
}
