using BackendContratos.Dtos;
using BackendContratos.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : Controller
{
    private readonly AuthServices _authServices;

    public AuthController(AuthServices authServices)
    {
        _authServices = authServices;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRegisterDto dto)
    {
        if (await _authServices.EmailExistsAsync(dto.Email))
            return BadRequest(new { message = "El correo ya está registrado" });

        await _authServices.RegisterAsync(dto);
        return Ok(new { message = "Usuario registrado exitosamente" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginDto dto)
    {
        var (token, user) = await _authServices.AuthenticateAsync(dto);

        if (string.IsNullOrEmpty(token))
            return Unauthorized("Credenciales inválidas");

        return Ok(new
        {
            token,
            user = new
            {
                id = user!.Id,
                nombre = user.Nombre,
                email = user.Email,
                role = user.Role
            }
        });
    }
}
