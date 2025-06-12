
using AutoMapper;
using InventarioComercio.Application.Contracts.Persistence;
using InventarioComercio.Application.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioComercio.Application.Features.Usuarios.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, string>
    {

        private readonly ILogger<DeleteUserCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitofWork;

        public DeleteUserCommandHandler(ILogger<DeleteUserCommandHandler> logger, IMapper mapper, IUnitOfWork unitofWork)
        {
            _logger = logger;
            _mapper = mapper;
            _unitofWork = unitofWork;
        }

        public async Task<string> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            // Buscar por Id 
            var existUser =await _unitofWork.UserRepository.GetByGuidAsync(request.Id);
            if(existUser == null)
            {
                _logger.LogWarning("No existe usuario con ese ID");
                throw new NotFoundException($"Usuario con ID {request.Id} no fue encontrado.");
                
            }

            _unitofWork.UserRepository.DeleteEntity(existUser);
            var result = await _unitofWork.Complete(); 

            if(result <= 0)
            {
                throw new Exception("No se pudo eliminar el usuario por error de servidor"); 
            }

            _logger.LogInformation($"Usuario eliminado! {existUser.Username}");
            return existUser.Username; 

        }
    }
}
