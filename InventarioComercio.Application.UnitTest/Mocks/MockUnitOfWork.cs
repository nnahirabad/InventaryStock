using InventarioComercio.Application.Contracts.Persistence;
using InventarioComercio.Infrastructure;
using InventarioComercio.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;


namespace InventarioComercio.Application.UnitTest.Mocks
{
    public static class MockUnitOfWork
    {


        public static (Mock<IUnitOfWork> mockUow, InventarioComercioDbContext context) GetUnitOfWork()
        {
            Guid dbContextId = Guid.NewGuid();

            var options = new DbContextOptionsBuilder<InventarioComercioDbContext>()
                .UseInMemoryDatabase(databaseName: $"InventarioDbContext-{dbContextId}")
                .Options;

            var context = new InventarioComercioDbContext(options);
            context.Database.EnsureDeleted(); // Limpiar para que cada test arranque limpio
            context.Database.EnsureCreated();

            // Creamos el mock del UnitOfWork y configuramos los repos
            var mockUow = new Mock<IUnitOfWork>();

            // Ejemplo: si necesitás que devuelva un repo real (opcional)
            var productRepo = new ProductRepository(context);
            mockUow.Setup(u => u.ProductRepository).Returns(productRepo);
            mockUow.Setup(u => u.CategoryRepository).Returns(new CategoryRepository(context));
            mockUow.Setup(u => u.Complete()).ReturnsAsync(() => context.SaveChanges());

            return (mockUow, context);
        }
    }
}
