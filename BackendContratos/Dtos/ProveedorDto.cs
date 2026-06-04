using System.ComponentModel.DataAnnotations;

namespace BackendContratos.Dtos
{
    public class ProveedorDto
    {
        public int Id { get; set; }
        public string? Nit { get; set; }
        public string? Nombre { get; set; }
        public string? RepresentanteLegal { get; set; }
    }

    public class ProveedorCreateDto
    {
        [Required(ErrorMessage = "El NIT es obligatorio")]
        [MaxLength(20)]
        public string Nit { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(200)]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El representante legal es obligatorio")]
        [MaxLength(200)]
        public string RepresentanteLegal { get; set; } = string.Empty;
    }

    public class ProveedorUpdateDto
    {
        [Required(ErrorMessage = "El NIT es obligatorio")]
        [MaxLength(20)]
        public string Nit { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(200)]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El representante legal es obligatorio")]
        [MaxLength(200)]
        public string RepresentanteLegal { get; set; } = string.Empty;
    }
}
