using System.Diagnostics.Contracts;

namespace BackendContratos.Models
{
    public class Proveedor
    {
        public int Id { get; set; }
        public string Nit { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string RepresentanteLegal { get; set; } = string.Empty;

        // Relación con contratos
        public ICollection<Contrato> Contratos { get; set; } = new List<Contrato>();
    }
}
