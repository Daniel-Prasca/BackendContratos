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
    public class LiquidacionesController : ControllerBase
    {
        private readonly LiquidacionesService _liquidacionesService;

        public LiquidacionesController(LiquidacionesService liquidacionesService)
        {
            _liquidacionesService = liquidacionesService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LiquidacionDto>>> GetAll()
        {
            return Ok(await _liquidacionesService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var liquidacion = await _liquidacionesService.GetByIdAsync(id);
            if (liquidacion == null) return NotFound(new { message = "Liquidación no encontrada" });
            return Ok(liquidacion);
        }

        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<LiquidacionDto>>> GetByUsuarioId(int usuarioId)
        {
            var liquidaciones = await _liquidacionesService.GetAllByUsuarioIdAsync(usuarioId);
            return Ok(liquidaciones);
        }


        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<ActionResult<LiquidacionDto>> Create(LiquidacionCreateDto dto)
        {
            var liquidacion = await _liquidacionesService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = liquidacion.Id }, liquidacion);
        }

        [Authorize(Roles = "User, Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, LiquidacionUpdateDto dto)
        {
            var updated = await _liquidacionesService.UpdateAsync(id, dto);
            if (!updated) return NotFound();
            return Ok(new { message = "Liquidación actualizada correctamente" });
        }

        [Authorize(Roles = "Admin, User")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _liquidacionesService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return Ok(new { message = "Liquidación eliminada correctamente" });
        }
    }
}
