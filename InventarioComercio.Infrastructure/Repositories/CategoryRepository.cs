using InventarioComercio.Application.Contracts.Persistence;
using InventarioComercio.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InventarioComercio.Infrastructure.Repositories
{
    public class CategoryRepository : RepositoryBase<Categoria>, ICategoryRepository 
    {

        


        public CategoryRepository(InventarioComercioDbContext context) : base(context)
        {
           
        }

        public async Task<Categoria> GetByGuidAsync(Guid id)
        {
            var categoria = await _context.Categorias!.FindAsync(id);
            return categoria; 
        }

        public async Task<List<Categoria>> GetAllAsync(Expression<Func<Categoria, object>> include=null)
        {
            var query = _context.Categorias.AsQueryable();
            if (include != null)
                query = query.Include(include);

            return await query.ToListAsync();
        }
    }
}
