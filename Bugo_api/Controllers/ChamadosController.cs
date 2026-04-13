using Microsoft.AspNetCore.Mvc;
using Bugo_shared.Models;
using Bugo_shared.Enum;
using Bugo_blazor.Services;

namespace Bugo_blazor.Controllers
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
        public async Task<IActionResult> Get()
        {
            var chamados = await _service.GetAll();
            return Ok(chamados);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Chamado chamado)
        {
            var novo = await _service.Create(chamado);
            return CreatedAtAction(nameof(GetById), new { id = novo.Id }, novo);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var chamado = await _service.GetById(id);
            if (chamado == null)
                return NotFound();

            return Ok(chamado);
        }

        [HttpGet("Abertos")]
        public async Task<IActionResult> GetAbertos()
        {
            var chamados = await _service.GetAberto();
            return Ok(chamados);
        }

        [HttpGet("tecnico/{id}")]
        public async Task<IActionResult> GetPorTecnico(int id)
        {
            var chamados = await _service.GetPorTecnico(id);
            return Ok(chamados);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] StatusChamado status)
        {
            var chamado = await _service.GetById(id);
            if (chamado == null)
                return NotFound();

            chamado.Status = status;
            await _service.Update(chamado);

            return Ok(chamado);
        }

        [HttpPut("{id}/assumir")]
        public async Task<IActionResult> Assumir(int id, [FromBody] int tecnicoId)
        {
            var chamado = await _service.AssumirChamado(id, tecnicoId);

            if (chamado == null)
                return NotFound();

            return Ok(chamado);
        }

        [HttpPut("{id}/finalizar")]
        public async Task<IActionResult> Finalizar(int id)
        {
            var chamado = await _service.FinalizarChamado(id);

            if (chamado == null)
                return NotFound();

            return Ok(chamado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var chamado = await _service.Delete(id);

            if (chamado == null)
                return NotFound();

            return Ok(chamado);
        }
    }
}
