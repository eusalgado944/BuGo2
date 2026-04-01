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
            return Ok(_service.GetAll());
        }

        [HttpPost]
        public IActionResult Post([FromBody] Usuario usuario)
        {
            var novo = _service.Create(usuario);
            return Ok(novo);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Usuario user)
        {
            var usuario = _service.Login(user.Email, user.Senha);
            if (usuario == null)
                return Unauthorized();
            
            return Ok(usuario);
        }
    }
}
