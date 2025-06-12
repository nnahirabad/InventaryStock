using AutoMapper;
using InventarioComercio.Application.Contracts.Persistence;
using InventarioComercio.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioComercio.Application.Features.Productos.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Guid>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateProductCommandHandler> _logger;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IUnitOfWork unitOfWork, ILogger<UpdateProductCommandHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Guid> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            // Logica 
            // 1. De todos los productos,tomar el seleccionado en request. 
            var producto = await _unitOfWork.ProductRepository.GetProductoById(request.Id); 
            if(producto == null)
            {
                _logger.LogError("El producto no existe");
                throw new Exception("El producto no existe"); 

            }
            
            producto.Nombre = request.Nombre;
            producto.Descripcion = request.Descripcion;
            producto.Codigo = request.Codigo;

            var categoria = await _unitOfWork.CategoryRepository.GetByGuidAsync(request.CategoriaId); 
            if(categoria == null)
            {
                _logger.LogError("Categoria no existe");
                throw new Exception("Categoria no existe");
            }
            producto.Categoria = (Categoria)categoria;

           _unitOfWork.ProductRepository.UpdateEntity(producto);
            var result = await _unitOfWork.Complete(); 
            if(result <= 0)
            {
                _logger.LogError("Error al actualizar el producto.");
                throw new Exception("No se pudo actualizar el producto.");
            }

            _logger.LogInformation($"Producto con ID {producto.Id} actualizado correctamente.");
            return producto.Id;

        }
    }
}
