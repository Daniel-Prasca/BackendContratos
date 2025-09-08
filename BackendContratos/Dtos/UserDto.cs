namespace BackendContratos.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }

    public class UserRegisterDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = "User"; // por defecto
    }

    public class UserLoginDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class UserUpdateDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
