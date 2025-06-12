
using AutoMapper;
using InventarioComercio.Application.Contracts.Persistence;
using InventarioComercio.Domain;
using MediatR;
using Microsoft.Extensions.Logging;


namespace InventarioComercio.Application.Features.Categorias.Commands.CreateCategorie
{
    public class CreateCategorieCommandHandler : IRequestHandler<CreateCategorieCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCategorieCommandHandler> _logger; 
        public CreateCategorieCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateCategorieCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger; 
        }

        public async Task<string> Handle(CreateCategorieCommand request, CancellationToken cancellationToken)
        {
            var nuevaCategoria = new Categoria
            {
                Nombre = request.Nombre
            };

            var categoriaMap = _mapper.Map<Categoria>(nuevaCategoria);
            _unitOfWork.CategoryRepository.AddEntity(categoriaMap);
            var result = await _unitOfWork.Complete(); 
            if(result <= 0)
            {
                throw new Exception("No se pudo crear la categoria"); 
            }

            _logger.LogInformation($"Se ha creado la categoria {request.Nombre}");
            return categoriaMap.Nombre; 

            
            
        }
    }
}
