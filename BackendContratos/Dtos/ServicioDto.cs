namespace BackendContratos.DTOs
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
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int ContratoId { get; set; }
    }

    public class ServicioUpdateDto
    {
        public string Nombre { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public int ContratoId { get; set; }
    }
}
