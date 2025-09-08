using BackendContratos.Data;
using BackendContratos.Dtos;
using BackendContratos.DTOs;
using BackendContratos.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendContratos.Services
{
    public class PolizasService
    {
        private readonly BackendContratoDbContext _context;

        public PolizasService(BackendContratoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PolizaDto>> GetAllAsync()
        {
            return await _context.Polizas
                .Select(p => new PolizaDto
                {
                    Id = p.Id,
                    ContratoId = p.ContratoId,
                    Tipo = p.Tipo,
                   FechaVencimiento = p.FechaVencimiento,
                   Estado = p.Estado
                })
                .ToListAsync();
        }

        public async Task<PolizaDto?> GetByIdAsync(int id)
        {
            var poliza = await _context.Polizas.FindAsync(id);
            if (poliza == null) return null;

            return new PolizaDto
            {
                Id = poliza.Id,
                ContratoId = poliza.ContratoId,
                Tipo = poliza.Tipo,
                FechaVencimiento = poliza.FechaVencimiento,
                Estado = poliza.Estado
            };
        }

        public async Task<PolizaDto> CreateAsync(PolizaCreateDto dto)
        {
            var poliza = new Poliza
            {
                ContratoId = dto.ContratoId,
                Tipo = dto.Tipo,
                FechaVencimiento = dto.FechaVencimiento,
                Estado = dto.Estado
            };

            _context.Polizas.Add(poliza);
            await _context.SaveChangesAsync();

            return new PolizaDto
            {
                Id = poliza.Id,
                ContratoId = poliza.ContratoId,
                Tipo = poliza.Tipo,
               FechaVencimiento = poliza.FechaVencimiento,
                Estado = poliza.Estado
            };
        }

        public async Task<bool> UpdateAsync(int id, PolizaUpdateDto dto)
        {
            var poliza = await _context.Polizas.FindAsync(id);
            if (poliza == null) return false;

            poliza.Tipo = dto.Tipo;
            poliza.FechaVencimiento = dto.FechaVencimiento;
            poliza.Estado = dto.Estado;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var poliza = await _context.Polizas.FindAsync(id);
            if (poliza == null) return false;

            _context.Polizas.Remove(poliza);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
