using AutoMapper;
using InventarioComercio.Application.Contracts.Persistence;
using InventarioComercio.Application.Dtos;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioComercio.Application.Features.Productos.Queries.GetProductLowStock
{
    public class GetProductWithLowStockQueryHandler : IRequestHandler<GetProductWithLowStockQuery, List<ProductoDto>>
    {

        private readonly ILogger<GetProductWithLowStockQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitofWork;

        public GetProductWithLowStockQueryHandler(ILogger<GetProductWithLowStockQueryHandler> logger, IMapper mapper, IUnitOfWork unitofWork)
        {
            _logger = logger;
            _mapper = mapper;
            _unitofWork = unitofWork;
        }

        public async Task<List<ProductoDto>> Handle(GetProductWithLowStockQuery request, CancellationToken cancellationToken)
        {
            var producto = await _unitofWork.ProductRepository.GetProductosConStockBajo();
            if (!producto.Any())
            {
                _logger.LogInformation("No hay productos con stock bajo!"); 
            }
            var productosMap = _mapper.Map<List<ProductoDto>>(producto);

            return productosMap; 
        }
    }
}
