
using AutoMapper;
using InventarioComercio.Application.Contracts.Persistence;
using InventarioComercio.Application.Dtos;
using InventarioComercio.Domain;
using MediatR;
using Microsoft.Extensions.Logging;


namespace InventarioComercio.Application.Features.Categorias.Queries.GetCategories
{
    public class GetAllCategorieQueryHandler : IRequestHandler<GetAllCategoriesQuery, List<CategoriaDto>>
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllCategorieQueryHandler> _logger;

        public GetAllCategorieQueryHandler(IUnitOfWork unitofwork, IMapper mapper, ILogger<GetAllCategorieQueryHandler> logger)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<CategoriaDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categorias = await _unitofwork.CategoryRepository.GetAllAsync(include: c => c.Productos);
            if (categorias == null || !categorias.Any()) // Puede la lista estar vacia. 
            {
                _logger.LogWarning("Se consultaron categorias, pero no hay productos registrados");
                throw new Exception("No hay categorias disponibles");

            }
             
            return _mapper.Map<List<CategoriaDto>>(categorias);


        }
    }
}
