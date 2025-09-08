using BackendContratos.Dtos;
using BackendContratos.DTOs;
using BackendContratos.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendContratos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ContratosController : ControllerBase
    {
        private readonly ContratosService _contratosService;

        public ContratosController(ContratosService contratosService)
        {
            _contratosService = contratosService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContratoDto>>> GetAll()
        {
            return Ok(await _contratosService.GetAllAsync());
        }

       
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var contrato = await _contratosService.GetByIdAsync(id);
            if (contrato == null) return NotFound(new {mesagge = "Contrato no encontrado"});
            return Ok(contrato);
        }

        // Crear (sólo Admin)
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<ContratoDto>> Create(ContratoCreateDto dto)
        {
            var contrato = await _contratosService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = contrato.Id }, contrato);
        }

        //Actualizar (sólo Admin)
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ContratoUpdateDto dto)
        {
            var updated = await _contratosService.UpdateAsync(id, dto);
            if (!updated) return NotFound();
            return Ok(new {mensagge = "Contrato actualizado correctamente"});
        }

        //Eliminar (sólo Admin)
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _contratosService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return Ok(new { mensagge = "Contrato eliminado correctamente" });
        }
    }
}
