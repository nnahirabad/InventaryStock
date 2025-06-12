using AutoMapper;
using InventarioComercio.Application.Contracts.Persistence;
using InventarioComercio.Application.Features.Productos.Commands.CreateProduct;
using InventarioComercio.Application.Features.Productos.Commands.UpdateProduct;
using InventarioComercio.Application.Mappings;
using InventarioComercio.Application.UnitTest.Mocks;
using InventarioComercio.Domain;
using InventarioComercio.Infrastructure;
using Microsoft.Extensions.Logging;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioComercio.Application.UnitTest.Features.Products.UpdateProduct
{
    public class UpdateProductCommandHandlerXUnitTest
    {


        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _unitOfWork;

        private readonly Mock<ILogger<UpdateProductCommandHandler>> _logger;

        private readonly InventarioComercioDbContext _context;


        public UpdateProductCommandHandlerXUnitTest()
        {
            var (mockUow, dbContext) = MockUnitOfWork.GetUnitOfWork();
            _unitOfWork = mockUow;
            _context = dbContext;
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
            _logger = new Mock<ILogger<UpdateProductCommandHandler>>();

            MockProductRepository.AddDataProductRepository(_context);



        }

        [Fact] 

        public async Task UpdateProductCommand_updateProduct_returnsGuid()
        {
            var productoId = Guid.NewGuid();
            var categoria = new Categoria
            {
                Id = Guid.NewGuid(), 
                Nombre = "Limpieza"
            };

            var productoOriginal = new Producto
            {
                Id = productoId,
                Nombre = "Original",
                Descripcion = "Descripcion original",
                Codigo = "ABC123",
                CategoriaId = categoria.Id,
                Categoria = categoria
            };

            
            _context.Categorias!.Add(categoria);
            _context.Productos!.Add(productoOriginal);
            _context.SaveChanges(); 




            var productoUpdate = new UpdateProductCommand
            {
                Id = productoId,
                Nombre = "Cartones", 
                Descripcion = "Cartones 4x4", 
                CategoriaId = categoria.Id, 
                Codigo = "33FR"
            };

            var handler = new UpdateProductCommandHandler(_unitOfWork.Object, _logger.Object, _mapper);

            var result = await handler.Handle(productoUpdate, CancellationToken.None);

            // Assert 

            result.ShouldBeOfType<Guid>(); 
            

            var productoCambiado = _context.Productos!
                .FirstOrDefault(p => p.Id == result);

            productoCambiado.ShouldNotBeNull();
            productoCambiado.Nombre!.ShouldBe("Cartones"); 
        }
    }
}
