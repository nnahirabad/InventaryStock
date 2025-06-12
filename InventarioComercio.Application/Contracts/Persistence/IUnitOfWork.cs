using InventarioComercio.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioComercio.Application.Contracts.Persistence
{
    public interface  IUnitOfWork : IDisposable
    {

        IMovimientoStockRepository MovimientoStock { get;  }
        IProductRepository ProductRepository { get;  }

        ICategoryRepository CategoryRepository { get;  }

        IUserRepository UserRepository { get;  }

        IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : DomainBaseModel;

        Task<int> Complete(); 
    }
}
