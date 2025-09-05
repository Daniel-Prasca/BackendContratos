namespace BackendContratos.Models
{
    public class Contrato
    {
        public int Id { get; set; }
        public int ProveedorId { get; set; }
        public Proveedor? Proveedor { get; set; }

        public string Objeto { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        // Relación con servicios, liquidaciones y pólizas
        public ICollection<Servicio> Servicios { get; set; } = new List<Servicio>();
        public ICollection<Liquidacion> Liquidaciones { get; set; } = new List<Liquidacion>();
        public ICollection<Poliza> Polizas { get; set; } = new List<Poliza>();

    }
}
