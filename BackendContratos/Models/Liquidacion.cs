namespace BackendContratos.Models
{
    public class Liquidacion
    {
        public int Id { get; set; }
        public int ContratoId { get; set; }
        public Contrato Contrato { get; set; }

        public int ServicioId { get; set; }
        public Servicio Servicio { get; set; }

        public int UsuarioId { get; set; }
        public User Usuario { get; set; }

        public int Cantidad { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; } = "Pendiente"; // Pendiente, Aprobado, Rechazado
        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}
