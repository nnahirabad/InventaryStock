
using MediatR;


namespace InventarioComercio.Application.Features.Usuarios.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<string>
    {
        public Guid Id { get; set;  }

        public DeleteUserCommand(Guid id)
        {
            Id = id; 
        }

    }
}
