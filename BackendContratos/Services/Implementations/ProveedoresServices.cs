using BackendContratos.Data;
using BackendContratos.Dtos;
using BackendContratos.Models;
using BackendContratos.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackendContratos.Services.Implementations
{
    public class ProveedoresService : IProveedoresService
    {
        private readonly BackendContratoDbContext _ctx;

        public ProveedoresService(BackendContratoDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<IEnumerable<ProveedorDto>> GetAllAsync()
        {
            return await _ctx.Proveedores
                .Select(p => new ProveedorDto
                {
                    Id = p.Id,
                    Nit = p.Nit,
                    Nombre = p.Nombre,
                    RepresentanteLegal = p.RepresentanteLegal
                })
                .ToListAsync();
        }

        public async Task<ProveedorDto?> GetByIdAsync(int id)
        {
            var p = await _ctx.Proveedores.FindAsync(id);
            if (p == null) return null;

            return new ProveedorDto
            {
                Id = p.Id,
                Nit = p.Nit,
                Nombre = p.Nombre,
                RepresentanteLegal = p.RepresentanteLegal
            };
        }

        public async Task<ProveedorDto> CreateAsync(ProveedorCreateDto dto)
        {
            var entity = new Proveedor
            {
                Nit = dto.Nit,
                Nombre = dto.Nombre,
                RepresentanteLegal = dto.RepresentanteLegal
            };

            _ctx.Proveedores.Add(entity);
            await _ctx.SaveChangesAsync();

            return new ProveedorDto
            {
                Id = entity.Id,
                Nit = entity.Nit,
                Nombre = entity.Nombre,
                RepresentanteLegal = entity.RepresentanteLegal
            };
        }

        public async Task<bool> UpdateAsync(int id, ProveedorUpdateDto dto)
        {
            var p = await _ctx.Proveedores.FindAsync(id);
            if (p == null) return false;

            p.Nit = dto.Nit;
            p.Nombre = dto.Nombre;
            p.RepresentanteLegal = dto.RepresentanteLegal;

            await _ctx.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var p = await _ctx.Proveedores.FindAsync(id);
            if (p == null) return false;

            _ctx.Proveedores.Remove(p);
            await _ctx.SaveChangesAsync();
            return true;
        }
    }
}
