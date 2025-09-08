using BackendContratos.Dtos;
using BackendContratos.DTOs;
using BackendContratos.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendContratos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")] // solo admin gestiona usuarios
    public class UsersController : ControllerBase
    {
        private readonly UsersService _usersService;

        public UsersController(UsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
        {
            return Ok(await _usersService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var user = await _usersService.GetByIdAsync(id);
            if (user == null) return NotFound(new { message = "Usuario no encontrado" });
            return Ok(user);
        }

        [HttpPost("register")]
        [AllowAnonymous] // abierto para registro
        public async Task<ActionResult<UserDto>> Register(UserRegisterDto dto)
        {
            var user = await _usersService.RegisterAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UserUpdateDto dto)
        {
            var updated = await _usersService.UpdateAsync(id, dto);
            if (!updated) return NotFound();
            return Ok(new { message = "Usuario actualizado correctamente" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _usersService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return Ok(new { message = "Usuario eliminado correctamente" });
        }
    }
}
