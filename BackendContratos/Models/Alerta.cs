namespace BackendContratos.Models
{
    public class Alerta
    {
        public int Id { get; set; }
        public string Tipo { get; set; } = string.Empty; // Contrato o Poliza
        public string Mensaje { get; set; } = string.Empty;
        public DateTime Fecha { get; set; } = DateTime.Now;

        public int? ContratoId { get; set; }
        public Contrato Contrato { get; set; }

        public int? PolizaId { get; set; }
        public Poliza Poliza { get; set; }
    }
}
