using AutoMapper;
using InventarioComercio.Application.Contracts.Persistence;
using InventarioComercio.Application.Features.Productos.Commands.CreateProduct;
using InventarioComercio.Application.Features.Usuarios.Queries.Login;
using InventarioComercio.Application.Helpers;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace InventarioComercio.Application.UnitTest.Features.Login
{
    public class LoginQueryHandlerXUnitTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _unitOfWork;

        private readonly Mock<ILogger<LoginHandler>> _logger;

        private readonly InventarioComercioDbContext _context;

        public LoginQueryHandlerXUnitTest()
        {
            var (mockUow, dbContext) = MockUnitOfWork.GetUnitOfWork();
            _unitOfWork = mockUow;
            _context = dbContext;
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
            _logger = new Mock<ILogger<LoginHandler>>();



        }

        [Fact]
        public async Task Handle_ValidCredentials_ReturnsToken()
        {


            var mockTokenService = new Mock<JwtTokenGenerator>();

            var fakeUser = new Usuario
            {
                Id = Guid.NewGuid(),
                Email = "test@example.com",
                PasswordHash = "1234",
                Rol = "Vendedor", 
                Username = "test1"
            };
            _unitOfWork.Setup(u => u.UserRepository.GetByEmailAsync("test@example.com"))
                 .ReturnsAsync(fakeUser);

            mockTokenService.Setup(t => t.GenerateToken(fakeUser.Email, fakeUser.Username, fakeUser.Rol))
                            .Returns("fake-jwt-token");

            var handler = new LoginHandler(_unitOfWork.Object, _logger.Object, mockTokenService.Object);


            var query = new LoginQuery
            {
                Email = "test@example.com",
                Password = "1234"
            };

            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.IsSuccess.ShouldBeTrue();
            result.Data!.Token.ShouldBe("fake-jwt-token");
            result.Data.Email.ShouldBe("test@example.com");
        }

        [Fact]
        public async Task Handle_InvalidPassword_ThrowsException() {



            var mockTokenService = new Mock<JwtTokenGenerator>();

            var fakeUser = new Usuario
            {
                Id = Guid.NewGuid(),
                Email = "test@example.com",
                PasswordHash = "1234",
                Rol = "Vendedor",
                Username = "test1"
            };
            _unitOfWork.Setup(u => u.UserRepository.GetByEmailAsync("test@example.com"))
                 .ReturnsAsync(fakeUser);

            mockTokenService.Setup(t => t.GenerateToken(fakeUser.Email, fakeUser.Username, fakeUser.Rol))
                            .Returns("fake-jwt-token");

            var handler = new LoginHandler(_unitOfWork.Object, _logger.Object, mockTokenService.Object);


            var query = new LoginQuery
            {
                Email = "test@example.com",
                Password = "abc1234"
            };





            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => handler.Handle(query, CancellationToken.None));
            exception.Message.ShouldBe("Credenciales inválidas");

        }
    }

}

