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
    public class MovimientoStockRepository : RepositoryBase<MovimientoStock>, IMovimientoStockRepository
    {
        public MovimientoStockRepository(InventarioComercioDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<MovimientoStock>> GetMovimientosPorFecha(DateTime desde, DateTime hasta)
        {
            return await _context.MovimientoStocks.Where(m => m.Fecha >= desde && m.Fecha <= hasta)
                .Include(m => m.Producto).
                OrderByDescending(m=>m.Fecha)
                .ToListAsync();
        }

        public async Task<IEnumerable<MovimientoStock>> ObtenerMovimientosPorProducto(Guid productoId)
        {
            var movimientos = await _context.MovimientoStocks.Where(m=>m.ProductoId ==productoId ).ToListAsync();
            return movimientos; 
        }

        public async Task RegistrarMovimiento(MovimientoStock movimiento)
        {
            await _context.MovimientoStocks.AddAsync(movimiento);

        }

        public async Task<IEnumerable<MovimientoStock>> GetAllMovimientos()
        {
            var todos = await _context.MovimientoStocks.Include(d => d.Producto).
                ToListAsync();
            foreach (var m in todos)
            {
                Console.WriteLine($"Movimiento: {m.Id}, Fecha: {m.Fecha}");
            }
            return todos; 

            
        }
    }
}
