using AutoMapper;
using InventarioComercio.Application.Contracts.Persistence;
using InventarioComercio.Application.Dtos;
using InventarioComercio.Application.Features.Productos.Commands.DeleteProduct;
using InventarioComercio.Application.Features.Productos.Queries.GetAllProducts;
using InventarioComercio.Application.Features.Productos.Queries.GetProductLowStock;
using InventarioComercio.Application.Mappings;
using InventarioComercio.Application.UnitTest.Mocks;
using InventarioComercio.Domain;
using InventarioComercio.Infrastructure;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;


namespace InventarioComercio.Application.UnitTest.Features.Products.GetProducts
{
    public class GetProductsQueryHandlerXUnitTest
    {

        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _unitOfWork;

        private readonly Mock<ILogger<GetProductWithLowStockQueryHandler>> _logger;
        private readonly Mock<ILogger<GetAllProductsQueryHandler>> _logger2; 
        private readonly InventarioComercioDbContext _context;


        public GetProductsQueryHandlerXUnitTest()
        {
            var (mockUow, dbContext) = MockUnitOfWork.GetUnitOfWork();
            _unitOfWork = mockUow;
            _context = dbContext;
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
            _logger = new Mock<ILogger<GetProductWithLowStockQueryHandler>>();
            _logger2 = new Mock<ILogger<GetAllProductsQueryHandler>>(); 

            MockProductRepository.AddDataProductRepository(_context);



        }

        [Fact]
        public async Task Handle_GetAllProducts_ReturnsProductoDtoList()
        {

           

            var productsList = new List<Producto>
            {
                new Producto{
                Id = Guid.NewGuid(),
                Nombre = "Coca cola",
                Codigo = "500",
                StockActual = 45,
                StockMinimo = 2
              }, new Producto{
                Id = Guid.NewGuid(),
                Nombre = "Pepsi",
                Codigo = "501",
                StockActual = 2,
                StockMinimo = 3
              },
              new Producto{
                Id = Guid.NewGuid(),
                Nombre = "Speed cola",
                Codigo = "500",
                StockActual = 12,
                StockMinimo = 2
              }, new Producto{
                Id = Guid.NewGuid(),
                Nombre = "Pepsi",
                Codigo = "501",
                StockActual = 22,
                StockMinimo = 3
              }
            };

            var mockProductRepo = new Mock<IProductRepository>();
            mockProductRepo
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(productsList);

            // Inyectar el mock en el UnitOfWork
            _unitOfWork.Setup(uow => uow.ProductRepository)
                       .Returns(mockProductRepo.Object);

            var handler = new GetAllProductsQueryHandler(_logger2.Object, _mapper, _unitOfWork.Object);
            var result = await handler.Handle(new GetAllProductsQuery(), CancellationToken.None);

            result.ShouldBeOfType<List<ProductoDto>>();
            result.ShouldNotBeNull(); 



        }

        [Fact]
        public async Task Handle_WithProductsWithLowStock_ReturnsProductoDtoList()
        {
            var productsList = new List<Producto>
            {
              new Producto{
                Id = Guid.NewGuid(),
                Nombre = "Coca cola",
                Codigo = "500",
                StockActual = 1,
                StockMinimo = 2
              }, new Producto{
                Id = Guid.NewGuid(),
                Nombre = "Pepsi",
                Codigo = "501",
                StockActual = 2,
                StockMinimo = 3
              }

            };

            var mockProductRepo = new Mock<IProductRepository>();
            mockProductRepo
                .Setup(repo => repo.GetProductosConStockBajo())
                .ReturnsAsync(productsList);

            // Inyectar el mock en el UnitOfWork
            _unitOfWork.Setup(uow => uow.ProductRepository)
                       .Returns(mockProductRepo.Object);

            var handler = new GetProductWithLowStockQueryHandler(_logger.Object, _mapper, _unitOfWork.Object);

            var result = await handler.Handle(new GetProductWithLowStockQuery(), CancellationToken.None);

            result.ShouldNotBeNull();
            result.ShouldBeOfType<List<ProductoDto>>();
            result.Count.ShouldBe(2);




        }

        [Fact]

        public async Task Handle_NoProductsLowStock_ReturnsListEmpty()
        {


            var emptyProductsList = new List<Producto>();
            

            var mockProductRepo = new Mock<IProductRepository>();
            mockProductRepo
                .Setup(repo => repo.GetProductosConStockBajo())
                .ReturnsAsync(emptyProductsList);

            // Inyectar el mock en el UnitOfWork
            _unitOfWork.Setup(uow => uow.ProductRepository)
                       .Returns(mockProductRepo.Object);

            var handler = new GetProductWithLowStockQueryHandler(_logger.Object, _mapper, _unitOfWork.Object);
            var result = await handler.Handle(new GetProductWithLowStockQuery(), CancellationToken.None);

            result.ShouldNotBeNull(); 
            result.ShouldBeEmpty(); 



        }
    }
}
