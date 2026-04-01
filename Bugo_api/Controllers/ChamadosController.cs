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

        [HttpGet("Abertos")]
        public IActionResult GetAbertos()
        {
            return Ok(_service.GetAberto());
        }

        [HttpGet("tecnico/{id}")]
        public IActionResult GetPorTecnico(int id)
        {
            return Ok(_service.GetPorTecnico(id));
        }

        [HttpPut("{id}/status")]
        public IActionResult UpdateStatus(int id, [FromBody] StatusChamado status)
        {
            if (status == null)
                return BadRequest("Status inválido");

            var chamado = _service.GetAll().FirstOrDefault(x => x.Id == id);
            if (chamado == null)
                return NotFound();

            chamado.Status = status;
            _service.Update(chamado);

            return Ok(chamado);
        }

        [HttpPut("{id}/assumir")]
        public IActionResult Assumir(int id, [FromBody] int tecnicoId)
        {
            var chamado = _service.AssumirChamado(id, tecnicoId);

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
