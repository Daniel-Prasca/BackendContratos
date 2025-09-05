using BackendContratos.Data;
using BackendContratos.DTOs;
using BackendContratos.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackendContratos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ServiciosController : ControllerBase
    {
        private readonly BackendContratoDbContext _context;

        public ServiciosController(BackendContratoDbContext context)
        {
            _context = context;
        }

        // GET: api/Servicios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServicioDto>>> GetServicios()
        {
            var servicios = await _context.Servicios
                .Select(s => new ServicioDto
                {
                    Id = s.Id,
                    Nombre = s.Nombre,
                    Precio = s.Precio,
                    //Cantidad = s.cantidad,
                })
                .ToListAsync();

            return Ok(servicios);
        }

        // GET: api/Servicios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServicioDto>> GetServicio(int id)
        {
            var servicio = await _context.Servicios
                .Where(s => s.Id == id)
                .Select(s => new ServicioDto
                {
                    Id = s.Id,
                    Nombre = s.Nombre,
                    Precio = s.Precio,
                    Cantidad = s.cantidad,
                })
                .FirstOrDefaultAsync();

            if (servicio == null)
                return NotFound();

            return Ok(servicio);
        }

        // POST: api/Servicios
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServicioDto>> PostServicio(ServicioDto servicioDto)
        {
            // Verificar que exista el contrato
            var contrato = await _context.Contratos.FindAsync(servicioDto.ContratoId);
            if (contrato == null)
                return BadRequest("El contrato especificado no existe.");

            var servicio = new Servicio
            {
                Nombre = servicioDto.Nombre,
                Precio = servicioDto.Precio,
                cantidad = servicioDto.Cantidad,
                ContratoId = servicioDto.ContratoId
            };

            _context.Servicios.Add(servicio);
            await _context.SaveChangesAsync();

            servicioDto.Id = servicio.Id;
            return CreatedAtAction(nameof(GetServicio), new { id = servicio.Id }, servicioDto);
        }


        // PUT: api/Servicios/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutServicio(int id, ServicioDto servicioDto)
        {
            if (id != servicioDto.Id)
                return BadRequest();

            var servicio = await _context.Servicios.FindAsync(id);
            if (servicio == null)
                return NotFound();

            servicio.Nombre = servicioDto.Nombre;
            servicio.Precio = servicioDto.Precio;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Servicios/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteServicio(int id)
        {
            var servicio = await _context.Servicios.FindAsync(id);
            if (servicio == null)
                return NotFound();

            _context.Servicios.Remove(servicio);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
