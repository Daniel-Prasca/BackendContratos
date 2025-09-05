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
    public class PolizasController : ControllerBase
    {
        private readonly BackendContratoDbContext _context;
        public PolizasController(BackendContratoDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PolizaDto>>> GetPolizas()
        {
            return await _context.Polizas
                .Select(p => new PolizaDto
                {
                    Id = p.Id,
                    ContratoId = p.ContratoId,
                    Tipo = p.Tipo,
                    FechaVencimiento = p.FechaVencimiento,
                    Estado = p.Estado
                }).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PolizaDto>> GetPoliza(int id)
        {
            var poliza = await _context.Polizas.FindAsync(id);
            if (poliza == null)
            {
                return NotFound("Póliza no encontrada.");
            }
            var dto = new PolizaDto
            {
                Id = poliza.Id,
                ContratoId = poliza.ContratoId,
                Tipo = poliza.Tipo,
                FechaVencimiento = poliza.FechaVencimiento,
                Estado = poliza.Estado
            };
            return dto;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<PolizaDto>> PostPoliza(PolizaDto dto)
        {
            if (!await _context.Contratos.AnyAsync(c => c.Id == dto.ContratoId))
            {
                return BadRequest("Contrato inválido.");
            }

            var poliza = new Poliza
            {
                ContratoId = dto.ContratoId,
                Tipo = dto.Tipo,
                FechaVencimiento = dto.FechaVencimiento,
                Estado = dto.Estado
            };

            _context.Polizas.Add(poliza);
            await _context.SaveChangesAsync();

            dto.Id = poliza.Id;
            return CreatedAtAction(nameof(GetPolizas), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutPoliza(int id, PolizaDto dto)
        {
            var poliza = await _context.Polizas.FindAsync(id);
            if (poliza == null)
            {
                return NotFound("Póliza no encontrada.");
            }
            
            poliza.ContratoId = dto.ContratoId;
            poliza.Tipo = dto.Tipo;
            poliza.FechaVencimiento = dto.FechaVencimiento;
            poliza.Estado = dto.Estado;
            await _context.SaveChangesAsync();
            return Ok(new { message = "Poliza actualizada correctamente" });
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePoliza(int id)
        {
            var poliza = await _context.Polizas.FindAsync(id);
            if (poliza == null)
            {
                return NotFound("Póliza no encontrada.");
            }
            _context.Polizas.Remove(poliza);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Poliza eliminada correctamente" });
        }
    }
}
