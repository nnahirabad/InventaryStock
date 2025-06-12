using AutoMapper;
using InventarioComercio.Application.Contracts.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;


namespace InventarioComercio.Application.Features.Productos.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteProductCommandHandler> _logger;
        private readonly IMapper _mapper;

        public DeleteProductCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteProductCommandHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            // --- Logica ----- 
            // 1-Verificar si id del request existe. Si no, mensaje con error
            // 2-Eliminar en repositorio
            // 3-Guardar cambios 
            // 4-Mensaje de exito. 

            // 1 -
            var entidadExiste = await _unitOfWork.ProductRepository.GetProductoById(request.Id); 
            if(entidadExiste == null)
            {
                _logger.LogWarning("No existe el producto con ese ID no se pudo eliminar");
                throw new Exception($"El producto con id: {request.Id} ");
                
            }
            // 2- 
             _unitOfWork.ProductRepository.DeleteEntity(entidadExiste);
            // 3- 
            var result = await _unitOfWork.Complete();
            if (result <= 0)
            {
                _logger.LogError("No se pudo eliminar");
                throw new Exception("No se elimino el producto de la base de datos"); 
            }

            _logger.LogInformation($"Se ha eliminado el producto con Id:{request.Id}");
            return entidadExiste.Id; 


        }
    }
}
