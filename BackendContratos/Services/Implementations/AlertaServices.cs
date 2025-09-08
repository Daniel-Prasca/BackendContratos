using BackendContratos.Data;
using BackendContratos.Dtos;
using BackendContratos.DTOs;
using BackendContratos.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendContratos.Services
{
    public class AlertasService
    {
        private readonly BackendContratoDbContext _context;

        public AlertasService(BackendContratoDbContext context)
        {
            _context = context;
        }

        // Listar todas las alertas
        public async Task<IEnumerable<AlertaDto>> GetAllAsync()
        {
            return await _context.Alertas
                .Select(a => new AlertaDto
                {
                    Id = a.Id,
                    Mensaje = a.Mensaje,
                    Fecha = a.Fecha,
                    ContratoId = a.ContratoId
                })
                .ToListAsync();
        }

        // Obtener por Id
        public async Task<AlertaDto?> GetByIdAsync(int id)
        {
            var alerta = await _context.Alertas.FindAsync(id);
            if (alerta == null) return null;

            return new AlertaDto
            {
                Id = alerta.Id,
                Mensaje = alerta.Mensaje,
                Fecha = alerta.Fecha,
                ContratoId = alerta.ContratoId
            };
        }

        // Crear
        public async Task<AlertaDto> CreateAsync(AlertaCreateDto dto)
        {
            var alerta = new Alerta
            {
                Mensaje = dto.Mensaje,
                Fecha = dto.Fecha,
                ContratoId = dto.ContratoId
            };

            _context.Alertas.Add(alerta);
            await _context.SaveChangesAsync();

            return new AlertaDto
            {
                Id = alerta.Id,
                Mensaje = alerta.Mensaje,
                Fecha = alerta.Fecha,
                ContratoId = alerta.ContratoId
            };
        }

        // Actualizar
        public async Task<bool> UpdateAsync(int id, AlertaUpdateDto dto)
        {
            var alerta = await _context.Alertas.FindAsync(id);
            if (alerta == null) return false;

            alerta.Mensaje = dto.Mensaje;
            alerta.Fecha = dto.Fecha;

            await _context.SaveChangesAsync();
            return true;
        }

        // Eliminar
        public async Task<bool> DeleteAsync(int id)
        {
            var alerta = await _context.Alertas.FindAsync(id);
            if (alerta == null) return false;

            _context.Alertas.Remove(alerta);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
