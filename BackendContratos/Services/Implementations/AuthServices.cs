using BackendContratos.Data;
using BackendContratos.Dtos;
using BackendContratos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BackendContratos.Services
{
    public class AuthServices
    {
        private readonly BackendContratoDbContext _context;
        private readonly IConfiguration _config;

        public AuthServices(BackendContratoDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }


        public async Task RegisterAsync(UserRegisterDto dto)
        {
            var user = new User
            {
                Nombre = dto.Nombre,
                Email = dto.Email,
                Password = dto.Password, // Guardar directamente el texto plano
                Role = dto.Role
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<(string Token, User? User)> AuthenticateAsync(UserLoginDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null) return (string.Empty, null);

            // Comparar directamente en texto plano
            if (user.Password != dto.Password) return (string.Empty, null);

            var token = GenerateJwtToken(user);
            return (token, user);
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Nombre),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task SeedUsersAsync()
        {
            if (!await _context.Users.AnyAsync())
            {
                var users = new List<User>
                {
                    new User { Nombre = "Administrador", Email = "admin@contratos.com", Password = "12345", Role = "Admin" },
                    new User { Nombre = "Usuario Demo", Email = "usuario@contratos.com", Password = "12345", Role = "User" }
                };
                _context.Users.AddRange(users);
                await _context.SaveChangesAsync();
            }
        }
    }
}
