namespace BackendContratos.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "User"; // User o Admin
        // Relación con liquidaciones creadas por este usuario
        public ICollection<Liquidacion> Liquidaciones { get; set; } = new List<Liquidacion>();

    }
}
