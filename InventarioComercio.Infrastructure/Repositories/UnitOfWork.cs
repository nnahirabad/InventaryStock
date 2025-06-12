using InventarioComercio.Application.Contracts.Persistence;
using InventarioComercio.Domain.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioComercio.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {

        private Hashtable _repositories;

        private readonly InventarioComercioDbContext _context; 

        private IProductRepository _productRepository;
        private ICategoryRepository _categoryRepository;
        private IUserRepository _userRepository;
        private IMovimientoStockRepository _movimientoStock; 

        // inyeccion via propriedades 


        public IProductRepository ProductRepository => _productRepository ??= new ProductRepository(_context);

        public ICategoryRepository CategoryRepository => _categoryRepository ??=new CategoryRepository(_context);

        public IUserRepository UserRepository => _userRepository ??=new UserRepository(_context);

        public IMovimientoStockRepository MovimientoStock => _movimientoStock ??= new MovimientoStockRepository(_context);
        public UnitOfWork(InventarioComercioDbContext context)
        {
            _context = context; 
        }

        public InventarioComercioDbContext InventarioComercioDbContext => _context;

      

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync(); 
        }

        public void Dispose()
        {
            _context.Dispose(); 
        }

        public IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : DomainBaseModel
        {
            if (_repositories == null)
            {
                _repositories = new Hashtable();
            }

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(RepositoryBase<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);
                _repositories.Add(type, repositoryInstance);


            }

            return (IAsyncRepository<TEntity>)_repositories[type];
        }
    }
}
