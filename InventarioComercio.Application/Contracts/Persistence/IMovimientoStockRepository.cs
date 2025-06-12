using InventarioComercio.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioComercio.Application.Contracts.Persistence
{
    public interface  IMovimientoStockRepository : IAsyncRepository<MovimientoStock>
    {

        Task RegistrarMovimiento(MovimientoStock movimiento);

        Task<IEnumerable<MovimientoStock>> GetAllMovimientos(); 

        Task<IEnumerable<MovimientoStock>> ObtenerMovimientosPorProducto(Guid productoId);

        Task<IEnumerable<MovimientoStock>> GetMovimientosPorFecha(DateTime desde, DateTime hasta);

    }
}
