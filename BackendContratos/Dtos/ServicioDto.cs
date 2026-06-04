using System.ComponentModel.DataAnnotations;

namespace BackendContratos.Dtos
{
    public class ServicioDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public int ContratoId { get; set; }
        public string ContratoObjeto { get; set; } = string.Empty;
    }

    public class ServicioCreateDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(200)]
        public string Nombre { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal Precio { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "El contrato es obligatorio")]
        public int ContratoId { get; set; }
    }

    public class ServicioUpdateDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(200)]
        public string Nombre { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal Precio { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "El contrato es obligatorio")]
        public int ContratoId { get; set; }
    }
}
