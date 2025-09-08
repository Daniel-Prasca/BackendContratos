using BackendContratos.Data;
using BackendContratos.Dtos;
using BackendContratos.DTOs;
using BackendContratos.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendContratos.Services
{
    public class ContratosService
    {
        private readonly BackendContratoDbContext _context;

        public ContratosService(BackendContratoDbContext context)
        {
            _context = context;
        }

        // Listar todos los contratos
        public async Task<IEnumerable<ContratoDto>> GetAllAsync()
        {
            return await _context.Contratos
                .Select(c => new ContratoDto
                {
                    Id = c.Id,
                    ProveedorId = c.ProveedorId,
                    Objeto = c.Objeto,
                    FechaInicio = c.FechaInicio,
                    FechaFin = c.FechaFin,
                    ProveedorNombre = c.Proveedor.Nombre
                })
                .ToListAsync();
        }

        // Obtener contrato por Id
        public async Task<ContratoDto?> GetByIdAsync(int id)
        {
            var contrato = await _context.Contratos
                .Include(c => c.Proveedor) 
                .FirstOrDefaultAsync(c => c.Id == id);

            if (contrato == null) return null;

            return new ContratoDto
            {
                Id = contrato.Id,
                ProveedorId = contrato.ProveedorId,
                Objeto = contrato.Objeto,
                FechaInicio = contrato.FechaInicio,
                FechaFin = contrato.FechaFin,
                ProveedorNombre = contrato.Proveedor?.Nombre 
            };
        }


        //  Crear contrato
        public async Task<ContratoDto> CreateAsync(ContratoCreateDto dto)
        {
            var contrato = new Contrato
            {
                ProveedorId = dto.ProveedorId,
                Objeto = dto.Objeto,
                FechaInicio = dto.FechaInicio,
                FechaFin = dto.FechaFin
            };

            _context.Contratos.Add(contrato);
            await _context.SaveChangesAsync();

            return new ContratoDto
            {
                Id = contrato.Id,
                ProveedorId = contrato.ProveedorId,
                Objeto = contrato.Objeto,
                FechaInicio = contrato.FechaInicio,
                FechaFin = contrato.FechaFin
            };
        }

        //  Actualizar contrato
        public async Task<bool> UpdateAsync(int id, ContratoUpdateDto dto)
        {
            var contrato = await _context.Contratos.FindAsync(id);
            if (contrato == null) return false;

            contrato.ProveedorId = dto.ProveedorId;
            contrato.Objeto = dto.Objeto;
            contrato.FechaInicio = dto.FechaInicio;
            contrato.FechaFin = dto.FechaFin;

            await _context.SaveChangesAsync();
            return true;
        }

        //  Eliminar contrato
        public async Task<bool> DeleteAsync(int id)
        {
            var contrato = await _context.Contratos.FindAsync(id);
            if (contrato == null) return false;

            _context.Contratos.Remove(contrato);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
