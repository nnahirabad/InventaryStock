
using InventarioComercio.Application.Dtos;
using InventarioComercio.Application.Dtos.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioComercio.Application.Features.Usuarios.Queries.Login
{
    public class LoginQuery : IRequest<ResponseDto<AuthResponse>>
    {
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = null!; 


 
    }
}
