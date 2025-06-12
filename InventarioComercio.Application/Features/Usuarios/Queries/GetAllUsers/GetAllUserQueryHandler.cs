
using AutoMapper;
using InventarioComercio.Application.Contracts.Persistence;
using InventarioComercio.Application.Dtos;
using InventarioComercio.Application.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioComercio.Application.Features.Usuarios.Queries.GetAllUsers
{
    public class GetAllUserQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetAllUserQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetAllUserQueryHandler(IUnitOfWork unitOfWork, ILogger<GetAllUserQueryHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var usuarios = await _unitOfWork.UserRepository.GetAllAsync();
            if(usuarios == null || !usuarios.Any())
            {
                _logger.LogWarning("Se consultaron usuarios pero no hay registrados");
                throw new NotFoundException("No hay usuarios registrados"); 
            }

            var usuariosMap = _mapper.Map<List<UserDto>>(usuarios);
            return usuariosMap; 
        }
    }
}
