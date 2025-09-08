namespace BackendContratos.Dtos
{
    public class ContratoDto
    {
        public int Id { get; set; }
        public string? Objeto { get; set; }
        public int ProveedorId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        // Relación con proveedor
        public string? ProveedorNombre { get; set; }
    }
    public class ContratoCreateDto
    {
        public int ProveedorId { get; set; }
        public string Objeto { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }

    public class ContratoUpdateDto
    {
        public string Objeto { get; set; }
        public int ProveedorId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }


}
