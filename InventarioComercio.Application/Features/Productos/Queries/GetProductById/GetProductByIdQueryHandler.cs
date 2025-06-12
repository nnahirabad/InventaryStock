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

namespace InventarioComercio.Application.Features.Productos.Queries.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, List<ProductoDto>>
    {

        private readonly ILogger<GetProductByIdQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitofWork;

        public GetProductByIdQueryHandler(ILogger<GetProductByIdQueryHandler> logger, IMapper mapper, IUnitOfWork unitofWork)
        {
            _logger = logger;
            _mapper = mapper;
            _unitofWork = unitofWork;
        }
        public async Task<List<ProductoDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var productos = await _unitofWork.ProductRepository.GetByGuidAsync(request.Id);
            if (productos==null)
            {
                throw new Exception($"No existe producto con ID {request.Id}"); 

            }

            var productoMap = _mapper.Map<List<ProductoDto>>(productos);
            return productoMap; 




            
        }
    }
}
