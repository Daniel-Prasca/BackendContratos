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
    public class AlertasController : ControllerBase
    {
        private readonly AlertasService _alertasService;

        public AlertasController(AlertasService alertasService)
        {
            _alertasService = alertasService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlertaDto>>> GetAll()
        {
            return Ok(await _alertasService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var alerta = await _alertasService.GetByIdAsync(id);
            if (alerta == null) return NotFound(new { message = "Alerta no encontrada" });
            return Ok(alerta);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<AlertaDto>> Create(AlertaCreateDto dto)
        {
            var alerta = await _alertasService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = alerta.Id }, alerta);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, AlertaUpdateDto dto)
        {
            var updated = await _alertasService.UpdateAsync(id, dto);
            if (!updated) return NotFound();
            return Ok(new { message = "Alerta actualizada correctamente" });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _alertasService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return Ok(new { message = "Alerta eliminada correctamente" });
        }
    }
}
