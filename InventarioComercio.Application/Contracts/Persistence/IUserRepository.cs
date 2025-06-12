using InventarioComercio.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioComercio.Application.Contracts.Persistence
{
    public interface IUserRepository : IAsyncRepository<Usuario>
    {

        Task<Usuario?> GetByEmailAsync(string email);
        Task<bool> ExistByUsername(string username); 


        

    }
}
