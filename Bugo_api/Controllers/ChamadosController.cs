using Microsoft.AspNetCore.Mvc;
using Bugo_shared.Models;
using Bugo_shared.Enum;
using Bugo_api.Services;

namespace Bugo_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChamadosController : ControllerBase
    {
        private readonly ChamadoService _service;

        public ChamadosController(ChamadoService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_service.GetAll());
        }

        [HttpPost]
        public IActionResult Post([FromBody] Chamado chamado)
        {
            var novo = _service.Create(chamado);
            return Ok(novo);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var chamado = _service.GetAll().FirstOrDefault(x => x.Id == id);
            if (chamado == null)
                return NotFound();

            return Ok(chamado);
        }

        [HttpPut("{id}/status")]
        public IActionResult UpdateStatus(int id, [FromBody] StatusChamado status)
        {
            var chamado = _service.GetAll().FirstOrDefault(x => x.Id == id);
            if (chamado == null)
                return NotFound();
            chamado.Status = status;
         
            return Ok(chamado);
        }

        [HttpPut("{id}/assumir")]
        public IActionResult Assumir(int id, [FromBody] string Tecnico)
        {
            var chamado = _service.AssumirChamado(id, Tecnico);

            if (chamado == null)
                return NotFound();

            return Ok(chamado);
        }

        [HttpPut("{id}/finalizar")]
        public IActionResult Finalizar(int id)
        {
            var chamado = _service.FinalizarChamado(id);
            
            if (chamado == null)
                return NotFound();
            
            return Ok(chamado);
        }
    }
}
