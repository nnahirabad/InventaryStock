using AutoFixture;
using InventarioComercio.Domain;
using InventarioComercio.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioComercio.Application.UnitTest.Mocks
{
    public static class MockProductRepository
    {

        public static void AddDataProductRepository(InventarioComercioDbContext inventarioFake)
        {
            var fixture = new Fixture();
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var products = fixture.CreateMany<Producto>().ToList();

            products.Add(fixture.Build<Producto>()
                .Without(a => a.MovimientosStock)
                .Without(a => a.CategoriaId)
                .Without(a=>a.Categoria)
                .Create());

            inventarioFake.Productos!.AddRange(products);
            inventarioFake.SaveChanges(); 
        }

    }
}
