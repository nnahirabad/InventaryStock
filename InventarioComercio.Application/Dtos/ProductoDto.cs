using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioComercio.Application.Dtos
{
    public class ProductoDto 
    {


        public Guid Id { get; set; } 
        public string Codigo { get; set; } = string.Empty; 
        public string Nombre { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;


        public int StockActual { get; set; }
        public string Categoria { get; set; } = string.Empty;

        public int StockMinimo { get; set; }



    }
}
