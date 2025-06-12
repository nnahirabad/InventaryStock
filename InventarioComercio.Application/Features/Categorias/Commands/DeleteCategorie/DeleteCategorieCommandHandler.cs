
using AutoMapper;
using InventarioComercio.Application.Contracts.Persistence;
using InventarioComercio.Application.Exceptions;
using InventarioComercio.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioComercio.Application.Features.Categorias.Commands.DeleteCategorie
{
    public class DeleteCategorieCommandHandler : IRequestHandler<DeleteCategorieCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteCategorieCommandHandler> _logger;
        private readonly IMapper _mapper;

        public DeleteCategorieCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteCategorieCommandHandler> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<string> Handle(DeleteCategorieCommand request, CancellationToken cancellationToken)
        {
            var categoriaFind = await _unitOfWork.CategoryRepository.GetByGuidAsync(request.Id); 
            if(categoriaFind == null)
            {
                _logger.LogError("No se encontro la categoria con ese id");
                throw new NotFoundException("No se encontro la categoria"); 
            }

            
            _unitOfWork.CategoryRepository.DeleteEntity(categoriaFind);
            var result = await _unitOfWork.Complete(); 
            if(result <=0)
            {
                _logger.LogError("No se pudo eliminar");
                throw new Exception("No se pudo eliminar"); 
            }

            return categoriaFind.Nombre; 
        }
    }
}
