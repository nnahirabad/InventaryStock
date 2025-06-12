using AutoMapper;
using InventarioComercio.Application.Contracts.Persistence;
using InventarioComercio.Application.Features.Productos.Commands.CreateProduct;
using InventarioComercio.Application.Mappings;
using InventarioComercio.Application.UnitTest.Mocks;
using InventarioComercio.Domain;
using InventarioComercio.Infrastructure;
using InventarioComercio.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioComercio.Application.UnitTest.Features.Products.CreateProduct
{
    public  class CreateProductCommandHandlerXUnitTest
    {

        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _unitOfWork;
       
        private readonly Mock<ILogger<CreateProductCommandHandler>> _logger;

        private readonly InventarioComercioDbContext _context; 

        public CreateProductCommandHandlerXUnitTest()
        {
            var (mockUow, dbContext) = MockUnitOfWork.GetUnitOfWork();
            _unitOfWork = mockUow;
            _context = dbContext;
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
            _logger = new Mock<ILogger<CreateProductCommandHandler>>();

            MockProductRepository.AddDataProductRepository(_context);

           

        }

        [Fact]

        public async Task CreateProductCommand_InputProduct_ReturnsGuid()
        {
            // Creamos el objeto input 
            var productInput = new CreateProductCommand
            {
                Nombre = "Detergente",
                Descripcion = "Producto de limpieza",
                Codigo = "500", 
                StockActual = 6, 
                StockMinimo = 1
            };

            var handler = new CreateProductCommandHandler(_unitOfWork.Object, _logger.Object, _mapper);
            var result = await handler.Handle(productInput, CancellationToken.None);

            result.ShouldBeOfType<Guid>();

            // Validación de que fue insertado en el DbContext (opcional pero muy útil)
            var productoInsertado = _context.Productos!
                .FirstOrDefault(p => p.Id == result);

            productoInsertado.ShouldNotBeNull();
        }

        
        [Fact]
        public async Task Handle_DuplicateProductName_ThrowsException()
        {
            // Arrange
           

            var fakeProductList = new List<Producto>
    {
        new Producto
        {
            Id = Guid.NewGuid(),
            Nombre = "Detergente",
            Descripcion = "Producto de limpieza",
            Codigo = "500",
            StockActual = 10,
            StockMinimo = 2,
            CategoriaId = Guid.NewGuid()
        }
    };

            _unitOfWork.Setup(u => u.ProductRepository.GetProductByName("Detergente"))
                          .ReturnsAsync(fakeProductList);

            var handler = new CreateProductCommandHandler(_unitOfWork.Object, _logger.Object,_mapper);

            var command = new CreateProductCommand
            {
                Nombre = "Detergente", // Duplicado
                Descripcion = "Nuevo producto",
                Codigo = "501",
                StockActual = 5,
                StockMinimo = 1
            };

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => handler.Handle(command, CancellationToken.None));
        }



    }
}
