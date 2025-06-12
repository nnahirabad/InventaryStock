
using InventarioComercio.Application.Contracts.Persistence;
using InventarioComercio.Domain;

using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioComercio.Application.Features.MovimientoStock.Commands
{
    public class RegistrarMovimientoHandler : IRequestHandler<RegistrarMovimientoCommand, bool>
    {
        private readonly IProductRepository _productoRepository;

        private readonly IMovimientoStockRepository _movimientoStockRepository;

        private readonly IUnitOfWork _unitOfWork;

        private readonly ILogger<RegistrarMovimientoHandler> _logger; 


        public RegistrarMovimientoHandler(IProductRepository productoRepository, IMovimientoStockRepository movimientoStockRepository, IUnitOfWork unit, ILogger<RegistrarMovimientoHandler> logger)
        {
            _productoRepository = productoRepository;
            _movimientoStockRepository = movimientoStockRepository;
            _unitOfWork = unit;
            _logger = logger; 
        }

        public async Task<bool> Handle(RegistrarMovimientoCommand request, CancellationToken cancellationToken)
        {
            //OBTENER EL PRODUCTO 
            var producto = await _productoRepository.GetProductoById(request.ProductoId); 
            if(producto == null)
            {
                _logger.LogInformation($"No existe el producto con Id:{request.ProductoId}"); 
                throw new Exception("Producto no encontrado"); 
            }

            // VALIDAD TIPO DE MOVIMIENTO 
            if(request.Tipo == TipoMovimiento.Salida)
            {
                 

                if (producto.StockActual < request.Cantidad) {
                    _logger.LogInformation($"Stock insuficiente para realizar la salida. Stock actual{producto.StockActual}, Cantidad Salida:{request.Cantidad}");
                    throw new Exception("Stock insuficiente para realizar la salida");
                }
                    
                 
                producto.StockActual -= request.Cantidad;

            }
             
            else if (request.Tipo == TipoMovimiento.Entrada)
            {
                producto.StockActual += request.Cantidad;
            }
            else
            {
                _logger.LogError($"Tipo de movimiento invalido recibido: {request.Tipo}");
                throw new Exception("Tipo de movimiento no valido"); 
            }


                // Crear entidad Movimiento. 

                var movimiento = new InventarioComercio.Domain.MovimientoStock
                {
                    Id = Guid.NewGuid(),
                    ProductoId = request.ProductoId,
                    Tipo = request.Tipo,
                    Cantidad = request.Cantidad,
                    Fecha = DateTime.UtcNow,
                    Observacion = request.Observacion
                };


            // REGISTRAR MOVIMIENTO Y ACTUALIZAR PRODUCTO

              _unitOfWork.MovimientoStock.AddEntity(movimiento);
              _unitOfWork.ProductRepository.UpdateEntity(producto);

            // CONFIRMAR TRANSACCION 

            var result = await _unitOfWork.Complete(); 
            if(result <= 0)
            {
                _logger.LogError($"Error al registrar el movimiento para producto ID:{producto.Id}");
                throw new Exception("No se ha podido realizar el movimiento"); 
            }

            _logger.LogInformation($"Movimiento registrado correctamente para producto: {request.ProductoId}, StockActual: {producto.StockActual}"); 

            // DEVOLVER BOOL VERDADERO 
            return true; 
        }
    }
}
