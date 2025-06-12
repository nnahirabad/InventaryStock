using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioComercio.Application.Dtos
{
    public class UserDto 
    {

        public Guid Id { get; set;  } 
        public string Nombre { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Username { get; set; } = null!;
        
        public string Rol { get; set; } = null !;
    }
}
