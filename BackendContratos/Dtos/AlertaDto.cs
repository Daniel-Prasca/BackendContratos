using System.ComponentModel.DataAnnotations;

namespace BackendContratos.Dtos
{
    public class AlertaDto
    {
        public int Id { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string Mensaje { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public int? ContratoId { get; set; }
        public int? PolizaId { get; set; }
    }

    public class AlertaCreateDto
    {
        [Required(ErrorMessage = "El mensaje es obligatorio")]
        [MaxLength(500)]
        public string Mensaje { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha es obligatoria")]
        public DateTime Fecha { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "El contrato es obligatorio")]
        public int ContratoId { get; set; }
    }

    public class AlertaUpdateDto
    {
        [Required(ErrorMessage = "El mensaje es obligatorio")]
        [MaxLength(500)]
        public string Mensaje { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha es obligatoria")]
        public DateTime Fecha { get; set; }
    }
}
