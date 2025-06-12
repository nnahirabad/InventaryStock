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
    public class UserRepository : RepositoryBase<Usuario>, IUserRepository
    {
        public UserRepository(InventarioComercioDbContext context) : base(context)
        {

        }
        public async Task<bool> ExistByUsername(string username)
        {
            return await _context.Usuarios!.AnyAsync(u => u.Username == username); 

            
            
        }

        public async Task<Usuario?> GetByEmailAsync(string email)
        {
            var user = await _context.Usuarios!.FirstOrDefaultAsync(u => u.Email == email);
            return user; 
        }
    }
}
