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
    [Authorize] // 👈 todos requieren autenticación
    public class ContratosController : ControllerBase
    {
        private readonly BackendContratoDbContext _context;

        public ContratosController(BackendContratoDbContext context)
        {
            _context = context;
        }

        // GET api/contratos
        [HttpGet]
        public async Task<IActionResult> GetContratos()
        {
            var contratos = await _context.Contratos
                .Include(c => c.Proveedor)
                .Select(c => new ContratoDto
                {
                    Id = c.Id,
                    Objeto = c.Objeto,
                    FechaInicio = c.FechaInicio,
                    FechaFin = c.FechaFin,
                    ProveedorNombre = c.Proveedor.Nombre
                })
                .ToListAsync();

            return Ok(contratos);
        }

        // GET api/contratos/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContrato(int id)
        {
            var contrato = await _context.Contratos
                .Include(c => c.Proveedor)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (contrato == null)
                return NotFound();

            var dto = new ContratoDto
            {
                Id = contrato.Id,
                Objeto = contrato.Objeto,
                FechaInicio = contrato.FechaInicio,
                FechaFin = contrato.FechaFin,
                ProveedorNombre = contrato.Proveedor.Nombre
            };

            return Ok(dto);
        }

        // POST api/contratos
        [HttpPost]
        [Authorize(Roles = "Admin")] // solo Admin puede crear
        public async Task<IActionResult> CreateContrato([FromBody] Contrato contrato)
        {
            _context.Contratos.Add(contrato);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Contrato creado exitosamente" });
        }

        // PUT api/contratos/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")] // solo Admin puede editar
        public async Task<IActionResult> UpdateContrato(int id, [FromBody] Contrato contratoRequest)
        {
            var contrato = await _context.Contratos.FindAsync(id);

            if (contrato == null)
                return NotFound();

            contrato.Objeto = contratoRequest.Objeto;
            contrato.FechaInicio = contratoRequest.FechaInicio;
            contrato.FechaFin = contratoRequest.FechaFin;
            contrato.ProveedorId = contratoRequest.ProveedorId;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Contrato actualizado exitosamente" });
        }

        // DELETE api/contratos/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] // solo Admin puede eliminar
        public async Task<IActionResult> DeleteContrato(int id)
        {
            var contrato = await _context.Contratos.FindAsync(id);

            if (contrato == null)
                return NotFound();

            _context.Contratos.Remove(contrato);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Contrato eliminado exitosamente" });
        }
    }
}
