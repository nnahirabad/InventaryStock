
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

namespace InventarioComercio.Application.Features.MovimientoStock.Queries.GetMovimientoByFecha
{
    public class GetMovimientoByFechaQueryHandler : IRequestHandler<GetMovimientoByFechaQuery, List<MovimientoStockDto>>
    {
        private readonly IMovimientoStockRepository _movimientoStockRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetMovimientoByFechaQueryHandler> _logger;
        private readonly IUnitOfWork _unitOfWork; 


        public GetMovimientoByFechaQueryHandler(IMovimientoStockRepository repository, ILogger<GetMovimientoByFechaQueryHandler> logger, IUnitOfWork unitofwork, IMapper mapper)
        {
            _movimientoStockRepository = repository;
            _logger = logger;
            _unitOfWork = unitofwork;
            _mapper = mapper; 
        }

        public async Task<List<MovimientoStockDto>> Handle(GetMovimientoByFechaQuery request, CancellationToken cancellationToken)
        {

            var movimientos = await _movimientoStockRepository.GetMovimientosPorFecha(request.Desde, request.Hasta);
            
            if (!movimientos.Any())
            {
                _logger.LogInformation("No hay movimientos existenes para esos rangos"); 

            }
            return _mapper.Map<List<MovimientoStockDto>>(movimientos); 



        }
    }
}
