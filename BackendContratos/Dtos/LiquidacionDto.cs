using System.ComponentModel.DataAnnotations;

namespace BackendContratos.Dtos
{
    public class LiquidacionDto
    {
        public int Id { get; set; }
        public int ContratoId { get; set; }
        public int ServicioId { get; set; }
        public int UsuarioId { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public string ContratoObjeto { get; set; } = string.Empty;
        public string ServicioNombre { get; set; } = string.Empty;
        public string UsuarioNombre { get; set; } = string.Empty;
    }

    public class LiquidacionCreateDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "El contrato es obligatorio")]
        public int ContratoId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "El servicio es obligatorio")]
        public int ServicioId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "El usuario es obligatorio")]
        public int UsuarioId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0")]
        public int Cantidad { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "El total debe ser mayor a 0")]
        public decimal Total { get; set; }

        public string Estado { get; set; } = "Pendiente";
    }

    public class LiquidacionUpdateDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a 0")]
        public int Cantidad { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "El total debe ser mayor a 0")]
        public decimal Total { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio")]
        public string Estado { get; set; } = "Pendiente";
    }
}
