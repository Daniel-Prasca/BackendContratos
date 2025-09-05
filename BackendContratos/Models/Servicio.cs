namespace BackendContratos.Models
{
    public class Servicio
    {
        public int Id { get; set; }
        public int ContratoId { get; set; }
        public Contrato Contrato { get; set; }

        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int cantidad { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        // Relación con liquidaciones
        public ICollection<Liquidacion> Liquidaciones { get; set; } = new List<Liquidacion>();
    }
}
