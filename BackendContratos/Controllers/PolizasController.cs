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
    public class PolizasController : ControllerBase
    {
        private readonly PolizasService _polizasService;

        public PolizasController(PolizasService polizasService)
        {
            _polizasService = polizasService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PolizaDto>>> GetAll()
        {
            return Ok(await _polizasService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var poliza = await _polizasService.GetByIdAsync(id);
            if (poliza == null) return NotFound(new { message = "Póliza no encontrada" });
            return Ok(poliza);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<PolizaDto>> Create(PolizaCreateDto dto)
        {
            var poliza = await _polizasService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = poliza.Id }, poliza);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PolizaUpdateDto dto)
        {
            var updated = await _polizasService.UpdateAsync(id, dto);
            if (!updated) return NotFound();
            return Ok(new { message = "Póliza actualizada correctamente" });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _polizasService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return Ok(new { message = "Póliza eliminada correctamente" });
        }
    }
}
