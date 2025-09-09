using BackendContratos.Data;
using BackendContratos.Dtos;
using BackendContratos.DTOs;
using BackendContratos.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace BackendContratos.Services
{
    public class UsersService
    {
        private readonly BackendContratoDbContext _context;

        public UsersService(BackendContratoDbContext context)
        {
            _context = context;
        }

        // 🔑 Registrar usuario
        public async Task<UserDto> RegisterAsync(UserRegisterDto dto)
        {
            

            var user = new User
            {
                Nombre = dto.Nombre,
                Email = dto.Email,
                Password = dto.Password,
                Role = dto.Role
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserDto
            {
                Id = user.Id,
                Nombre = user.Nombre,
                Email = user.Email,
                Role = user.Role
            };
        }

        // 🔑 Login
        public async Task<User?> AuthenticateAsync(UserLoginDto dto)
        {
            using var sha256 = SHA256.Create();
            var hashedPassword = Convert.ToBase64String(
                sha256.ComputeHash(Encoding.UTF8.GetBytes(dto.Password))
            );

            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == dto.Email && u.Password == hashedPassword);
        }

        // Listar usuarios
        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            return await _context.Users
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Nombre = u.Nombre,
                    Email = u.Email,
                    Role = u.Role
                })
                .ToListAsync();
        }

        // Obtener por Id
        public async Task<UserDto?> GetByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return null;

            return new UserDto
            {
                Id = user.Id,
                Nombre = user.Nombre,
                Email = user.Email,
                Role = user.Role
            };
        }

        // Actualizar (nombre/rol)
        public async Task<bool> UpdateAsync(int id, UserUpdateDto dto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            user.Nombre = dto.Nombre;
            user.Role = dto.Role;

            await _context.SaveChangesAsync();
            return true;
        }

        // Eliminar
        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
