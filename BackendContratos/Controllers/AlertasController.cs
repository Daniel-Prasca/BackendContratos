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
    public class AlertasController : ControllerBase
    {
        private readonly BackendContratoDbContext _context;
        public AlertasController(BackendContratoDbContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlertaDto>>> GetAlertas()
        {
            return await _context.Alertas
                .Select(a => new AlertaDto
                {
                    Id = a.Id,
                    Tipo = a.Tipo,
                    Mensaje = a.Mensaje,
                    Fecha = a.Fecha,
                    ContratoId = a.ContratoId,
                    PolizaId = a.PolizaId
                }).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AlertaDto>> GetAlerta(int id)
        {
            var alerta = await _context.Alertas.FindAsync(id);
            if (alerta == null)
            {
                return NotFound("Alerta no encontrada.");
            }
            var dto = new AlertaDto
            {
                Id = alerta.Id,
                Tipo = alerta.Tipo,
                Mensaje = alerta.Mensaje,
                Fecha = alerta.Fecha,
                ContratoId = alerta.ContratoId,
                PolizaId = alerta.PolizaId
            };
            return dto;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<AlertaDto>> PostAlerta(AlertaDto dto)
        {
            var alerta = new Alerta
            {
                Tipo = dto.Tipo,
                Mensaje = dto.Mensaje,
                Fecha = dto.Fecha,
                ContratoId = dto.ContratoId,
                PolizaId = dto.PolizaId
            };

            _context.Alertas.Add(alerta);
            await _context.SaveChangesAsync();

            dto.Id = alerta.Id;
            return CreatedAtAction(nameof(GetAlertas), new { id = dto.Id }, dto);
        }

      
    }
}
