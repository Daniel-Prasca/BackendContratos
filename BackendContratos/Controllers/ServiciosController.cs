using BackendContratos.DTOs;
using BackendContratos.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendContratos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ServiciosController : ControllerBase
    {
        private readonly ServiciosService _serviciosService;

        public ServiciosController(ServiciosService serviciosService)
        {
            _serviciosService = serviciosService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServicioDto>>> GetAll()
        {
            return Ok(await _serviciosService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var servicio = await _serviciosService.GetByIdAsync(id);
            if (servicio == null) return NotFound(new { message = "Servicio no encontrado" });
            return Ok(servicio);
        }

        [Authorize(Roles = "Admin, User")]
        [HttpPost]
        public async Task<ActionResult<ServicioDto>> Create(ServicioCreateDto dto)
        {
            var servicio = await _serviciosService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = servicio.Id }, servicio);
        }

        [Authorize(Roles = "Admin, User")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ServicioUpdateDto dto)
        {
            var updated = await _serviciosService.UpdateAsync(id, dto);
            if (!updated) return NotFound();
            return Ok(new { message = "Servicio actualizado correctamente" });
        }

        [Authorize(Roles = "Admin, User")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _serviciosService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return Ok(new { message = "Servicio eliminado correctamente" });
        }
    }
}
