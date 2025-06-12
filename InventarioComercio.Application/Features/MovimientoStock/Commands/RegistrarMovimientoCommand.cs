using Amazon.Runtime.Internal;
using InventarioComercio.Domain;
using MediatR;


namespace InventarioComercio.Application.Features.MovimientoStock.Commands
{
    public class RegistrarMovimientoCommand : IRequest<bool>
    {

        public Guid ProductoId { get; set; }
        public TipoMovimiento Tipo { get; set; } // Entrada o Salida
        public int Cantidad { get; set; }
        public string? Observacion { get; set; }
    }
}
