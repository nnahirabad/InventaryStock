
using MediatR;


namespace InventarioComercio.Application.Features.Productos.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest<Guid>
    {

        public Guid Id { get; set;  }

        public DeleteProductCommand(Guid id)
        {
            Id = id; 
        }


    }
}
