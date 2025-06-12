using InventarioComercio.Application.Contracts.Persistence;
using InventarioComercio.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioComercio.Infrastructure.Repositories
{
    public class ProductRepository : RepositoryBase<Producto>, IProductRepository
    {
       

        public ProductRepository(InventarioComercioDbContext context) : base (context)
        {
           
        }
        public async Task DeleteProductById(Guid id)
        {

            var producto = await _context.Productos!.FindAsync(id); 
            if(producto != null)
            {
                _context.Productos.Remove(producto);
            }
            
        }

        public async Task<IEnumerable<Producto>> GetProductByName(string name)
        {
            return await _context.Productos!.Where(p => p.Nombre.Contains(name))
                         .Include(p => p.Categoria)
                         .ToListAsync(); 
        }

        public async Task<IEnumerable<Producto>> GetByCategoriaId(Guid categoriaId)
        {
            return await _context.Productos!.Where(p => p.CategoriaId == categoriaId)
                .Include(p=>p.Categoria).ToListAsync(); 
        }

        public async Task<IEnumerable<Producto>>GetProductosConStockBajo()
        {
            return await _context.Productos!.Where(p => p.StockActual < p.StockMinimo)
                .Include(p => p.Categoria)
                .ToListAsync(); 


        }

        public async Task<int> GetStockActual(Guid productoId)
        {
            var producto = await _context.Productos!
                              .AsNoTracking()
                              .FirstOrDefaultAsync(p => p.Id == productoId);
            if (producto == null)
                throw new KeyNotFoundException("Producto no encontrado");
            return producto.StockActual; 
        }

        public async Task UpdateStock(Guid productoId, int nuevaCantidad)
        {
            var producto = await _context.Productos!.FindAsync(productoId); 
            if(producto != null)
            {
                producto.StockActual = nuevaCantidad;
                _context.Productos!.Update(producto);
            }
            throw new KeyNotFoundException("Producto no encontrado");

        }

        public async Task<Producto> GetProductoById(Guid id)
        {
            var producto = await _context.Productos!.FindAsync(id);
            return producto; 
        }
    }
}
