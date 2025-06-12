
using AutoMapper;
using InventarioComercio.Application.Contracts.Persistence;
using InventarioComercio.Domain;
using MediatR;
using Microsoft.Extensions.Logging;


namespace InventarioComercio.Application.Features.Productos.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateProductCommandHandler> _logger;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IUnitOfWork unitOfWork, ILogger<CreateProductCommandHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {

            //1. Verificar si el nombre del producto  ya existe. 
            var productoExistente = await _unitOfWork.ProductRepository.GetProductByName(request.Nombre);
            if (productoExistente.Any())
            {
                _logger.LogWarning($"Ya existe un producto con el nombre: {request.Nombre}");
                throw new Exception("Ya existe un producto con el mismo nombre"); 

            }

            // 2. Mapear request a una entidad
            var productEntity = _mapper.Map<Producto>(request);
            // 3. Agregar producto al repositorio 
            _unitOfWork.ProductRepository.AddEntity(productEntity);
            // 4. Guardar cambios
            var result = await _unitOfWork.Complete(); 

            if(result <= 0)
            {
                _logger.LogError($"No se pudo crear el producto");
                throw new Exception("No se pudo crear el producto"); 

            }

            _logger.LogInformation($"Producto creado con éxito: {productEntity.Nombre} (ID: {productEntity.Id})");

            return productEntity.Id; 
        }
    }
}
