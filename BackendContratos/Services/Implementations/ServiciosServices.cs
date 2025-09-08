using BackendContratos.Data;
using BackendContratos.DTOs;
using BackendContratos.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendContratos.Services
{
    public class ServiciosService
    {
        private readonly BackendContratoDbContext _context;

        public ServiciosService(BackendContratoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ServicioDto>> GetAllAsync()
        {
            return await _context.Servicios
                .Include(s => s.Contrato)
                .Select(s => new ServicioDto
                {
                    Id = s.Id,
                    Nombre = s.Nombre,
                    Precio = s.Precio,
                    ContratoId = s.ContratoId,
                    ContratoObjeto = s.Contrato.Objeto
                })
                .ToListAsync();
        }

        public async Task<ServicioDto?> GetByIdAsync(int id)
        {
            var servicio = await _context.Servicios
                .Include(s => s.Contrato)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (servicio == null) return null;

            return new ServicioDto
            {
                Id = servicio.Id,
                Nombre = servicio.Nombre,
                Precio = servicio.Precio,
                ContratoId = servicio.ContratoId,
                ContratoObjeto = servicio.Contrato.Objeto
            };
        }

        public async Task<ServicioDto> CreateAsync(ServicioCreateDto dto)
        {
            var servicio = new Servicio
            {
                Nombre = dto.Nombre,
                Precio = dto.Precio,
                ContratoId = dto.ContratoId
            };

            _context.Servicios.Add(servicio);
            await _context.SaveChangesAsync();

            return new ServicioDto
            {
                Id = servicio.Id,
                Nombre = servicio.Nombre,
                Precio = servicio.Precio,
                ContratoId = servicio.ContratoId,
                ContratoObjeto = (await _context.Contratos.FindAsync(servicio.ContratoId))?.Objeto ?? ""
            };
        }

        public async Task<bool> UpdateAsync(int id, ServicioUpdateDto dto)
        {
            var servicio = await _context.Servicios.FindAsync(id);
            if (servicio == null) return false;

            servicio.Nombre = dto.Nombre;
            servicio.Precio = dto.Precio;
            servicio.ContratoId = dto.ContratoId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var servicio = await _context.Servicios.FindAsync(id);
            if (servicio == null) return false;

            _context.Servicios.Remove(servicio);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
