using System.ComponentModel.DataAnnotations;

namespace BackendContratos.Dtos
{
    public class ContratoDto
    {
        public int Id { get; set; }
        public string? Objeto { get; set; }
        public int ProveedorId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string? ProveedorNombre { get; set; }
    }

    public class ContratoCreateDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "El proveedor es obligatorio")]
        public int ProveedorId { get; set; }

        [Required(ErrorMessage = "El objeto del contrato es obligatorio")]
        [MaxLength(500)]
        public string Objeto { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha de inicio es obligatoria")]
        public DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "La fecha de fin es obligatoria")]
        public DateTime FechaFin { get; set; }
    }

    public class ContratoUpdateDto
    {
        [Required(ErrorMessage = "El objeto del contrato es obligatorio")]
        [MaxLength(500)]
        public string Objeto { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "El proveedor es obligatorio")]
        public int ProveedorId { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es obligatoria")]
        public DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "La fecha de fin es obligatoria")]
        public DateTime FechaFin { get; set; }
    }
}
