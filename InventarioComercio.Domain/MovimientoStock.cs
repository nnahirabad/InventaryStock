using InventarioComercio.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioComercio.Domain
{
    public  class MovimientoStock : DomainBaseModel
    {

        
        public Guid ProductoId { get; set; }
        public TipoMovimiento Tipo { get; set; } // Entrada o Salida
        public int Cantidad { get; set; }
        public DateTime Fecha { get; set; }
        public string? Observacion { get; set; }

        public Producto Producto { get; set; } = null!;
    }

    public enum TipoMovimiento
    {
        Entrada = 1, 
        Salida = 2
    }
}
