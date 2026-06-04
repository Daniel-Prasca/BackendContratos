using System.ComponentModel.DataAnnotations;

namespace BackendContratos.Dtos
{
    public class PolizaDto
    {
        public int Id { get; set; }
        public int ContratoId { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public DateTime FechaVencimiento { get; set; }
        public string Estado { get; set; } = string.Empty;
    }

    public class PolizaCreateDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "El contrato es obligatorio")]
        public int ContratoId { get; set; }

        [Required(ErrorMessage = "El tipo de póliza es obligatorio")]
        [MaxLength(100)]
        public string Tipo { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha de vencimiento es obligatoria")]
        public DateTime FechaVencimiento { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio")]
        public string Estado { get; set; } = string.Empty;
    }

    public class PolizaUpdateDto
    {
        [Required(ErrorMessage = "El tipo de póliza es obligatorio")]
        [MaxLength(100)]
        public string Tipo { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha de vencimiento es obligatoria")]
        public DateTime FechaVencimiento { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio")]
        public string Estado { get; set; } = string.Empty;
    }
}
