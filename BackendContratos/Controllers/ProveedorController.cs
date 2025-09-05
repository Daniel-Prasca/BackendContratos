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
    [Authorize] // 👈 asegura que todos los métodos requieren un usuario autenticado
    public class ProveedoresController : ControllerBase
    {
        private readonly BackendContratoDbContext _context;

        public ProveedoresController(BackendContratoDbContext context)
        {
            _context = context;
        }

        // GET api/proveedores
        [HttpGet]
        public async Task<IActionResult> GetProveedores()
        {
            var proveedores = await _context.Proveedores
                .Select(p => new ProveedorDto
                {
                    Id = p.Id,
                    Nit = p.Nit,
                    Nombre = p.Nombre,
                    RepresentanteLegal = p.RepresentanteLegal
                })
                .ToListAsync();

            return Ok(proveedores);
        }

        // GET api/proveedores/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProveedor(int id)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);

            if (proveedor == null)
                return NotFound();

            var dto = new ProveedorDto
            {
                Id = proveedor.Id,
                Nit = proveedor.Nit,
                Nombre = proveedor.Nombre,
                RepresentanteLegal = proveedor.RepresentanteLegal
            };

            return Ok(dto);
        }

        // POST api/proveedores
        [HttpPost]
        [Authorize(Roles = "Admin")] // solo Admin puede crear
        public async Task<IActionResult> CreateProveedor([FromBody] ProveedorDto request)
        {
            var proveedor = new Proveedor
            {
                Nit = request.Nit,
                Nombre = request.Nombre,
                RepresentanteLegal = request.RepresentanteLegal
            };

            _context.Proveedores.Add(proveedor);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Proveedor creado exitosamente" });
        }

        // PUT api/proveedores/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")] // solo Admin puede editar
        public async Task<IActionResult> UpdateProveedor(int id, [FromBody] ProveedorDto request)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);

            if (proveedor == null)
                return NotFound();

            proveedor.Nit = request.Nit;
            proveedor.Nombre = request.Nombre;
            proveedor.RepresentanteLegal = request.RepresentanteLegal;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Proveedor actualizado exitosamente" });
        }

        // DELETE api/proveedores/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] //  solo Admin puede eliminar
        public async Task<IActionResult> DeleteProveedor(int id)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);

            if (proveedor == null)
                return NotFound();

            _context.Proveedores.Remove(proveedor);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Proveedor eliminado exitosamente" });
        }
    }
}
