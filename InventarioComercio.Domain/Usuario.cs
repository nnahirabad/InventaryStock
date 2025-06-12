using InventarioComercio.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioComercio.Domain
{
    public class Usuario : DomainBaseModel
    {
       
        public string Nombre { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Rol { get; set; } = "Vendedor"; // o Admin
    }
}
