using Amazon.Runtime.Internal.Util;
using AutoMapper;
using InventarioComercio.Application.Contracts.Persistence;
using InventarioComercio.Application.Dtos;
using MediatR;
using Microsoft.Extensions.Logging;

namespace InventarioComercio.Application.Features.MovimientoStock.Queries.GetAllMovements
{
    public class GetAllMovementQueryHandler : IRequestHandler<GetAllMovementsQuery, List<MovimientoStockDto>>
    {
        private readonly IUnitOfWork _unitOFwork;
        private readonly ILogger<GetAllMovementQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetAllMovementQueryHandler(IUnitOfWork unitOFwork, ILogger<GetAllMovementQueryHandler> logger, IMapper mapper)
        {
            _unitOFwork = unitOFwork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<MovimientoStockDto>> Handle(GetAllMovementsQuery request, CancellationToken cancellationToken)
        {
            var movimientos = await _unitOFwork.MovimientoStock.GetAllMovimientos(); 
            if(movimientos == null)
            {
                _logger.LogInformation("Aun no hay movimientos");
                Console.WriteLine("Aun no hay movimientos");
                 
            }

             return _mapper.Map<List<MovimientoStockDto>>(movimientos);
            


        }
    }
}
