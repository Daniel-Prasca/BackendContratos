namespace BackendContratos.Dtos
{
    public class ContratoDto
    {
        public int Id { get; set; }
        public string? Objeto { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        // Relación con proveedor
        public string? ProveedorNombre { get; set; }
    }
}
