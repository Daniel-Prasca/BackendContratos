using BackendContratos.Data;
using BackendContratos.Dtos;
using BackendContratos.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendContratos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LiquidacionesController : ControllerBase
    {
        private readonly BackendContratoDbContext _context;
        public LiquidacionesController(BackendContratoDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LiquidacionDto>>> GetLiquidaciones()
        {
            return await _context.Liquidaciones
                .Select(l => new LiquidacionDto
                {
                    Id = l.Id,
                    ContratoId = l.ContratoId,
                    ServicioId = l.ServicioId,
                    UsuarioId = l.UsuarioId,
                    Cantidad = l.Cantidad,
                    Total = l.Total,
                    Estado = l.Estado,
                    Fecha = l.Fecha
                }).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LiquidacionDto>> GetLiquidacion(int id)
        {
            var liquidacion = await _context.Liquidaciones.FindAsync(id);
            if (liquidacion == null)
            {
                return NotFound("Liquidación no encontrada.");
            }
            var dto = new LiquidacionDto
            {
                Id = liquidacion.Id,
                ContratoId = liquidacion.ContratoId,
                ServicioId = liquidacion.ServicioId,
                UsuarioId = liquidacion.UsuarioId,
                Cantidad = liquidacion.Cantidad,
                Total = liquidacion.Total,
                Estado = liquidacion.Estado,
                Fecha = liquidacion.Fecha
            };
            return dto;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<LiquidacionDto>> PostLiquidacion(LiquidacionDto dto)
        {
            if (!await _context.Contratos.AnyAsync(c => c.Id == dto.ContratoId) ||
                !await _context.Servicios.AnyAsync(s => s.Id == dto.ServicioId) ||
                !await _context.Users.AnyAsync(u => u.Id == dto.UsuarioId))
            {
                return BadRequest("Contrato, Servicio o Usuario inválido.");
            }

            var liquidacion = new Liquidacion
            {
                ContratoId = dto.ContratoId,
                ServicioId = dto.ServicioId,
                UsuarioId = dto.UsuarioId,
                Cantidad = dto.Cantidad,
                Total = dto.Total,
                Estado = dto.Estado,
                Fecha = dto.Fecha
            };

            _context.Liquidaciones.Add(liquidacion);
            await _context.SaveChangesAsync();

            dto.Id = liquidacion.Id;
            return CreatedAtAction(nameof(GetLiquidaciones), new { id = dto.Id }, dto);

        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutLiquidacion(int id,[FromBody] LiquidacionDto dto)
        {
           var liquidacion = await _context.Liquidaciones.FindAsync(id);
              if (liquidacion == null)
              {
                return NotFound("Liquidación no encontrada.");
              }
                liquidacion.ContratoId = dto.ContratoId;
                liquidacion.ServicioId = dto.ServicioId;
                liquidacion.UsuarioId = dto.UsuarioId;
                liquidacion.Cantidad = dto.Cantidad;
                liquidacion.Total = dto.Total;
                liquidacion.Estado = dto.Estado;
                liquidacion.Fecha = dto.Fecha;
                await _context.SaveChangesAsync();
                return Ok(new { message = "Liquidación actualizada exitosamente" });

        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteLiquidacion(int id)
        {
            var liquidacion = await _context.Liquidaciones.FindAsync(id);
            if (liquidacion == null)
            {
                return NotFound("Liquidación no encontrada.");
            }
            _context.Liquidaciones.Remove(liquidacion);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Liquidación eliminada exitosamente" });
        }
    }
}
