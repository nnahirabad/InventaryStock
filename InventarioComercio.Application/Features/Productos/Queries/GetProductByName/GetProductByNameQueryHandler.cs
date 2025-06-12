
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

namespace InventarioComercio.Application.Features.Productos.Queries.GetProductByName
{
    public class GetProductByNameQueryHandler : IRequestHandler<GetProductByNameQuery, List<ProductoDto>>
    {

        private readonly ILogger<GetProductByNameQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetProductByNameQueryHandler(ILogger<GetProductByNameQueryHandler> logger, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ProductoDto>> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
        {
            var productos = await _unitOfWork.ProductRepository.GetProductByName(request.Name);
            if(productos == null || !productos.Any())
            {
                _logger.LogWarning($"No existe un producto con el nombre: {request.Name}");
                throw new Exception("No existe un producto con el nombre solicidtado"); 
            }

            var productoMap = _mapper.Map<List<ProductoDto>>(productos);
            return productoMap; 
        }
    }
}
