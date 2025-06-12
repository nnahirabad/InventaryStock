using InventarioComercio.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InventarioComercio.Application.Contracts.Persistence
{
    public  interface  ICategoryRepository : IAsyncRepository<Categoria>
    {

        Task<Categoria> GetByGuidAsync(Guid id);

        Task<List<Categoria>> GetAllAsync(Expression<Func<Categoria, object>> include);



    }
}
