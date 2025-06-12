using Amazon.Runtime.Internal;
using InventarioComercio.Application.Dtos;
using InventarioComercio.Application.Dtos.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioComercio.Application.Features.Usuarios.Commands.CreateUser
{
    public class RegisterCommand : IRequest<ResponseDto<AuthResponse>>
    {

        public string Nombre { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Rol { get; set; } = null!;
    }
}
