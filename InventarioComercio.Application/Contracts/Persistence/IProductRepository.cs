using InventarioComercio.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioComercio.Application.Contracts.Persistence
{
    public interface IProductRepository : IAsyncRepository<Producto>
    {
       
            Task<IEnumerable<Producto>> GetProductByName(string name);

            Task DeleteProductById(Guid id);

        Task<Producto> GetProductoById(Guid id); 


            Task<int> GetStockActual(Guid productoId);

        

            Task UpdateStock(Guid productoId, int nuevaCantidad);

            Task<IEnumerable<Producto>> GetByCategoriaId(Guid categoriaId);

            Task<IEnumerable<Producto>> GetProductosConStockBajo(); // stockActual < stockMinimo
        



    }
}
