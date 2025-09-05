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
}
