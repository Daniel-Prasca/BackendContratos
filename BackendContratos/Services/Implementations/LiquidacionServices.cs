using BackendContratos.Data;
using BackendContratos.Dtos;
using BackendContratos.DTOs;
using BackendContratos.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendContratos.Services
{
    public class LiquidacionesService
    {
        private readonly BackendContratoDbContext _context;

        public LiquidacionesService(BackendContratoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LiquidacionDto>> GetAllAsync()
        {
            return await _context.Liquidaciones
                .Include(l => l.Contrato)
                .Include(l => l.Servicio)
                .Include(l => l.Usuario)
                .Select(l => new LiquidacionDto
                {
                    Id = l.Id,
                    ContratoId = l.ContratoId,
                    ServicioId = l.ServicioId,
                    UsuarioId = l.UsuarioId,
                    Cantidad = l.Cantidad,
                    Total = l.Total,
                    Estado = l.Estado,
                    Fecha = l.Fecha,
                    ContratoObjeto = l.Contrato.Objeto,
                    ServicioNombre = l.Servicio.Nombre,
                    UsuarioNombre = l.Usuario.Nombre
                })
                .ToListAsync();
        }

        public async Task<LiquidacionDto?> GetByIdAsync(int id)
        {
            var liquidacion = await _context.Liquidaciones
                .Include(l => l.Contrato)
                .Include(l => l.Servicio)
                .Include(l => l.Usuario)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (liquidacion == null) return null;

            return new LiquidacionDto
            {
                Id = liquidacion.Id,
                ContratoId = liquidacion.ContratoId,
                ServicioId = liquidacion.ServicioId,
                UsuarioId = liquidacion.UsuarioId,
                Cantidad = liquidacion.Cantidad,
                Total = liquidacion.Total,
                Estado = liquidacion.Estado,
                Fecha = liquidacion.Fecha,
                ContratoObjeto = liquidacion.Contrato.Objeto,
                ServicioNombre = liquidacion.Servicio.Nombre,
                UsuarioNombre = liquidacion.Usuario.Nombre
            };
        }

        public async Task<LiquidacionDto> CreateAsync(LiquidacionCreateDto dto)
        {
            var liquidacion = new Liquidacion
            {
                ContratoId = dto.ContratoId,
                ServicioId = dto.ServicioId,
                UsuarioId = dto.UsuarioId,
                Cantidad = dto.Cantidad,
                Total = dto.Total,
                Estado = dto.Estado,
                Fecha = DateTime.Now
            };

            _context.Liquidaciones.Add(liquidacion);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(liquidacion.Id) ?? throw new Exception("Error al crear liquidación");
        }
        public async Task<IEnumerable<LiquidacionDto>> GetAllByUsuarioIdAsync(int usuarioId)
        {
            return await _context.Liquidaciones
                .Where(l => l.UsuarioId == usuarioId)
                .Include(l => l.Contrato)
                .Include(l => l.Servicio)
                .Include(l => l.Usuario)
                .Select(l => new LiquidacionDto
                {
                    Id = l.Id,
                    ContratoId = l.ContratoId,
                    ServicioId = l.ServicioId,
                    UsuarioId = l.UsuarioId,
                    Cantidad = l.Cantidad,
                    Total = l.Total,
                    Estado = l.Estado,
                    Fecha = l.Fecha,
                    ContratoObjeto = l.Contrato.Objeto,
                    ServicioNombre = l.Servicio.Nombre,
                    UsuarioNombre = l.Usuario.Nombre
                })
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(int id, LiquidacionUpdateDto dto)
        {
            var liquidacion = await _context.Liquidaciones.FindAsync(id);
            if (liquidacion == null) return false;

            liquidacion.Cantidad = dto.Cantidad;
            liquidacion.Total = dto.Total;
            liquidacion.Estado = dto.Estado;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var liquidacion = await _context.Liquidaciones.FindAsync(id);
            if (liquidacion == null) return false;

            _context.Liquidaciones.Remove(liquidacion);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
