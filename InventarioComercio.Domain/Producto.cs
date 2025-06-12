using InventarioComercio.Domain.Common;

namespace InventarioComercio.Domain
{
    public class Producto : DomainBaseModel
    {
        
        public string Nombre { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        public string Codigo { get; set; } = string.Empty; 

        public int StockActual { get; set;  }

        public int StockMinimo { get; set;  }

        public Guid CategoriaId { get; set; }

        public Categoria Categoria { get; set; } = null!;
        public ICollection<MovimientoStock> MovimientosStock { get; set; } = new List<MovimientoStock>();

    }
}
