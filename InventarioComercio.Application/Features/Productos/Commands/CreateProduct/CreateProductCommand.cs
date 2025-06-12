
using MediatR;


namespace InventarioComercio.Application.Features.Productos.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<Guid>
    {
        // Esta clase es la que recibe todos los parametros que envia el cliente

        public string Nombre { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        public string Codigo { get; set; } = string.Empty;

        public int StockActual { get; set; }

        public int StockMinimo { get; set; }

        public Guid CategoriaId { get; set; }


    }
}
