using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioComercio.Application.Dtos
{
    public  class MovimientoStockDto
    {
        
        public Guid Id { get; set; }
        public DateTime Fecha { get; set; }
        public int Cantidad { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string? Observacion { get; set; }
        public string NombreProducto { get; set; } = string.Empty;
    }
}
