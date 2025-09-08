using BackendContratos.Dtos;

namespace BackendContratos.Services.Interfaces
{
    public interface IProveedoresService
    {
        Task<IEnumerable<ProveedorDto>> GetAllAsync();
        Task<ProveedorDto?> GetByIdAsync(int id);
        Task<ProveedorDto> CreateAsync(ProveedorCreateDto dto);
        Task<bool> UpdateAsync(int id, ProveedorUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
