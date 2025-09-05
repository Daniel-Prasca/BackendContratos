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
    }
}
