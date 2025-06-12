
using AutoMapper;
using InventarioComercio.Application.Contracts.Persistence;
using InventarioComercio.Application.Dtos;
using MediatR;
using Microsoft.Extensions.Logging;


namespace InventarioComercio.Application.Features.Productos.Queries.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<ProductoDto>>
    {
        private readonly ILogger<GetAllProductsQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitofWork;

        public GetAllProductsQueryHandler(ILogger<GetAllProductsQueryHandler> logger, IMapper mapper, IUnitOfWork unitofWork)
        {
            _logger = logger;
            _mapper = mapper;
            _unitofWork = unitofWork;
        }

        public async Task<List<ProductoDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var productos = await _unitofWork.ProductRepository.GetAllAsync(); 
            if(productos == null || !productos.Any()) // Puede la lista estar vacia. 
            {
                _logger.LogWarning("Se consultaron productos, pero no hay productos registrados"); 
                throw new Exception("No hay productos disponibles"); 

            }
            var productosMap = _mapper.Map<List<ProductoDto>>(productos);
            return productosMap; 


        }
    }
}
