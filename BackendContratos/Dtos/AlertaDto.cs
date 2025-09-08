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
        public string Mensaje { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public int ContratoId { get; set; }
    }

    public class AlertaUpdateDto
    {
        public string Mensaje { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
    }
}
