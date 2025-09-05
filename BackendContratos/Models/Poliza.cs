namespace BackendContratos.Models
{
    public class Poliza
    {
        public int Id { get; set; }
        public int ContratoId { get; set; }
        public Contrato Contrato { get; set; }

        public string Tipo { get; set; } = string.Empty;
        public DateTime FechaVencimiento { get; set; }
        public string Estado { get; set; } = "Activa"; // Activa, Vencida
    }
}
