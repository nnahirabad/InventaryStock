using AutoMapper;
using InventarioComercio.Application.Contracts.Persistence;
using InventarioComercio.Application.Features.Productos.Commands.DeleteProduct;
using InventarioComercio.Application.Features.Productos.Commands.UpdateProduct;
using InventarioComercio.Application.Mappings;
using InventarioComercio.Application.UnitTest.Mocks;
using InventarioComercio.Domain;
using InventarioComercio.Infrastructure;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;


namespace InventarioComercio.Application.UnitTest.Features.Products.DeleteProduct
{
    public class DeleteProductCommandHandlerXUnitTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _unitOfWork;

        private readonly Mock<ILogger<DeleteProductCommandHandler>> _logger;

        private readonly InventarioComercioDbContext _context;

        public DeleteProductCommandHandlerXUnitTest()
        {
            var (mockUow, dbContext) = MockUnitOfWork.GetUnitOfWork();
            _unitOfWork = mockUow;
            _context = dbContext;
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
            _logger = new Mock<ILogger<DeleteProductCommandHandler>>();

            MockProductRepository.AddDataProductRepository(_context);



        }

        [Fact]

        public async Task DeleteProductCommandHandler_DeleteEntity_ReturnsGuid()
        {
            // Arrange 

            var productoId = Guid.NewGuid();

            var categoria = new Categoria
            {
                Id = Guid.NewGuid(),
                Nombre = "Alimentos"
            }; 
           

            var producto = new Producto()
            {
                Id = productoId, 
                Nombre = "Eliminar",
                Descripcion = "Producto a eliminar", 
                Codigo = "444C", 
                CategoriaId = categoria.Id,
                Categoria = categoria

            };

            _context.Categorias!.Add(categoria);
            _context.Productos!.Add(producto);
            _context.SaveChanges();

            var productoEliminar = new DeleteProductCommand(productoId)
            {
                Id = productoId
            };

            var handler = new DeleteProductCommandHandler(_unitOfWork.Object, _logger.Object, _mapper);

            var result = await handler.Handle(productoEliminar, CancellationToken.None);

            // Assert 

            result.ShouldBeOfType<Guid>(); 



        }

    }
}
